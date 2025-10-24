using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Implementations;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class AlquilerService : IAlquilerService
    {

        private readonly IAlquilerRepository _repo;
        private readonly IClienteRepository _repoClientes;
        private readonly IAuxiliarRepository<Sucursal> _repoSucursal;
        private readonly IVehiculoRepository _repoVehiculos;

        public AlquilerService(IAlquilerRepository repo, IClienteRepository repoClientes,
            IAuxiliarRepository<Sucursal> repoSucursal, IVehiculoRepository repoVehiculos)
        {
            _repo = repo;
            _repoClientes = repoClientes;
            _repoSucursal = repoSucursal;
            _repoVehiculos = repoVehiculos;
        }

        private Alquiler MapToBD(ModifyAlquilerDTO a)
        {
            Alquiler alquiler = new Alquiler
            {
                id_alquiler = a.id_alquiler,
                fecha_fin = a.fecha_fin,
                fecha_inicio = a.fecha_inicio,  
                monto = a.monto,
                id_sucursal = a.id_sucursal,
                id_vehiculo = a.id_vehiculo
            };

            if (a.id_clienteNavigation.id_cliente == 0)
            {
                alquiler.id_clienteNavigation = new Cliente
                {
                    id_cliente = a.id_clienteNavigation.id_cliente,
                    nombre = a.id_clienteNavigation.nombre,
                    documento = a.id_clienteNavigation.documento,
                    id_tipo_documento = a.id_clienteNavigation.id_tipo_documento,
                    Contactos = a.id_clienteNavigation.Contactos.Select(c => new Contacto
                    {
                        id_contacto = c.id_contacto,
                        contacto = c.contacto,
                        id_tipo_contacto = c.id_tipo_contacto
                    }).ToList()
                };
            }
            else
            {
                alquiler.id_cliente = a.id_clienteNavigation.id_cliente;
            }

            return alquiler;
        }

        private AlquilerDTO MapToDTO(Alquiler a)
        {
            return new AlquilerDTO
            {
                id_alquiler = a.id_alquiler,
                fecha_fin = a.fecha_fin,
                fecha_inicio = a.fecha_inicio,
                id_clienteNavigation = new ClienteDTO
                {
                    id_cliente = a.id_clienteNavigation.id_cliente,
                    documento = a.id_clienteNavigation.documento,
                    nombre = a.id_clienteNavigation.nombre,
                    id_tipo_documentoNavigation = new TipoDocDTO
                    {
                        id_tipo_documento = a.id_clienteNavigation.id_tipo_documentoNavigation.id_tipo_documento,
                        tipo = a.id_clienteNavigation.id_tipo_documentoNavigation.tipo
                    },
                    Contactos = a.id_clienteNavigation.Contactos.Select(c => new ContactoDTO
                    {
                        contacto = c.contacto,
                        id_tipo_contactoNavigation = c.id_tipo_contactoNavigation,
                        id_contacto = c.id_contacto
                    }).ToList()
                },
                monto = a.monto,
                id_sucursalNavigation = a.id_sucursalNavigation,
                id_vehiculoNavigation = a.id_vehiculoNavigation,
                id_estado_alquilerNavigation = a.id_estado_alquilerNavigation
            };
        }

        public async Task<ResponseApi> CambioEstado(int id, string estado)
        {
            try
            {
                Alquiler? alquiler = await _repo.GetById(id);
                if (alquiler != null)
                {
                    if (string.IsNullOrEmpty(estado))
                    {
                        if (await _repo.CambioEstado(id, estado))
                        {
                            return new ResponseApi(200, "Cambio de estado realizado con exito");
                        }
                        else { return new ResponseApi(400, "No se pudo realizar el cambio de estado"); }
                    }
                    else { return new ResponseApi(400, "Se debe enviar un estado valido para el alquiler"); }
                }
                else { return new ResponseApi(404, "Alquiler no encontrado"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        public async Task<ResponseApi> GetAll(string docCliente, string? estado)
        {
            try
            {
                if (await _repoClientes.GetByDocument(docCliente) != null)
                {
                    List<Alquiler>? alquileres = await _repo.GetAll(docCliente, estado);
                    if (alquileres != null && alquileres.Any())
                    {
                        return new ResponseApi(200, "Alquileres encontrados", alquileres.Select(a => MapToDTO(a)).ToList());
                    }
                    else { return new ResponseApi(404, "No se encontraron alquileres registrados"); }
                }
                else { return new ResponseApi(404, "No se encontro un cliente con ese numero de documento"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }

        }

        public async Task<ResponseApi> GetById(int id)
        {
            try
            {
                Alquiler? alquiler = await _repo.GetById(id);
                if (alquiler != null)
                {
                    return new ResponseApi(200, "Alquiler encontrado", MapToDTO(alquiler));
                }
                else { return new ResponseApi(404, "Alquiler no encontrado"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        // Completar Validaciones y Metodos Post y Put (Guiarse en base a VehiculoService)

        private async Task<string?> Validaciones(ModifyAlquilerDTO ma)
        {
            try
            {
                if (await _repo.GetById(ma.id_alquiler) == null && ma.id_alquiler > 0)
                {
                    return "No se pudo encontrar el alquiler";
                }
                //fecha de inicio no debe ser anterior al dia actual(hoy)
                if (ma.fecha_inicio < DateTime.Today)
                {
                    return "La fecha de inicio no puede ser anterior al dia actual";
                }

                // fecha fin no debe ser anterior a la fecha de incio
                
                if (ma.fecha_fin < ma.fecha_inicio)
                {
                    return "La fecha de fin no puede ser anterior a la fecha de inicio";
                }

                //el alquiler debe tener una duracion minima de 8 horas

                if (ma.fecha_fin.HasValue)
                {
                    if (ma.fecha_fin < ma.fecha_inicio.AddHours(8))
                    {
                        return "El alquiler debe durar al menos 8 horas";
                    }
                    //Esta es otra alternativa!
                    //TimeSpan? diferencia = ma.fecha_fin - ma.fecha_inicio;

                    //if (diferencia.Value.Hours < 8)
                    //{
                    //    return "El alquiler debe durar al menos 8 horas";
                    //}
                }

                Cliente? validarCliente = await _repoClientes.GetByDocument(ma.id_clienteNavigation.documento);

                if (validarCliente != null && validarCliente.id_cliente != ma.id_clienteNavigation.id_cliente)
                {
                    return "Ya existe un Cliente con este Documento";
                }

                //monto no debe ser menor a 0 
                if (ma.monto < 0)
                {
                    return "El monto no puede ser menor a 0";
                }

                // sucursal debe existir 
                if (await _repoSucursal.GetById(ma.id_sucursal) == null)
                {
                    return "La sucursal no existe";
                }

                //vehiculo debe existir
                if (await _repoVehiculos.GetById(ma.id_vehiculo) == null)
                {
                    return "El vehiculo no existe";
                }

                //en caso de que el idcliente sea mayor a 0 ver que exista (como en idcliente y estado) 

                if (await _repoClientes.GetById(ma.id_clienteNavigation.id_cliente) == null && ma.id_clienteNavigation.id_cliente > 0)
                {
                    return "El cliente no existe";
                }

                //

                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseApi> Post(ModifyAlquilerDTO ma)
        {
            string? validacion = await Validaciones(ma);
            ma.id_alquiler = 0;
            try
            {
                if (string.IsNullOrEmpty(validacion))
                {
                    if (await _repo.Post(MapToBD(ma)))
                    {
                        return new ResponseApi(200, "Alquiler guardado con exito", ma);
                    }
                    else { return new ResponseApi(400, "No se pudo guardar el alquiler", ma); }
                }
                else
                {
                    return new ResponseApi(400, validacion);
                }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        public async Task<ResponseApi> Put(ModifyAlquilerDTO ma)
        {
            string? validacion = await Validaciones(ma);
            try 
            {
                if (string.IsNullOrEmpty(validacion))
                {
                    if (await _repoClientes.Put(MapToBD(ma).id_clienteNavigation))
                    {
                        if (await _repo.Put(MapToBD(ma)))
                        {
                            return new ResponseApi(200, "Alquiler modificado con exito", ma);
                        }
                        else { return new ResponseApi(400, "No se pudo modificar el alquiler", ma); }
                    }
                    else { return new ResponseApi(400, "No se pudo modificar el Cliente"); }
                }
                else
                {
                    return new ResponseApi(400, validacion);
                }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }
    }
}
