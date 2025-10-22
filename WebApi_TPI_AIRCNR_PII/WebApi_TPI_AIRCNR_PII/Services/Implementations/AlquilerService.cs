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

        private readonly AlquilerRepository _repo;
        private readonly ClienteRepository _repoClientes;
        private readonly IAuxiliarRepository<Sucursal> _repoSucursal;
        private readonly VehiculoRepository _repoVehiculos;

        public AlquilerService(AlquilerRepository repo, ClienteRepository repoClientes,
            IAuxiliarRepository<Sucursal> repoSucursal, VehiculoRepository repoVehiculos)
        {
            _repo = repo;
            _repoClientes = repoClientes;
            _repoSucursal = repoSucursal;
            _repoVehiculos = repoVehiculos;
        }

        private Alquiler MapToBD(ModifyAlquilerDTO a)
        {
            return new Alquiler
            {
                id_alquiler = a.id_alquiler,
                fecha_fin = a.fecha_fin,
                fecha_inicio = a.fecha_inicio,
                id_clienteNavigation = new Cliente
                {
                    id_cliente = a.id_clienteNavigation.id_cliente,
                    nombre = a.id_clienteNavigation.nombre,
                    documento = a.id_clienteNavigation.documento,
                    id_tipo_documento = a.id_clienteNavigation.id_tipo_documento,
                    Contactos = a.id_clienteNavigation.Contactos
                },
                monto = a.monto,
                id_sucursal = a.id_sucursal,
                id_vehiculo = a.id_vehiculo
            };
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
                    Contactos = a.id_clienteNavigation.Contactos
                },
                monto = a.monto,
                id_sucursalNavigation = a.id_sucursalNavigation,
                id_vehiculoNavigation = a.id_vehiculoNavigation,
                id_estado_alquilerNavigation = a.id_estado_alquilerNavigation
            };
        }

        public async Task<ResponseApi> CambioEstado(int id, string estado)
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

        public async Task<ResponseApi> GetAll(string docCliente, string? estado)
        {
            List<Alquiler>? alquileres = await _repo.GetAll(docCliente, estado);
            if (alquileres != null && alquileres.Any())
            {
                return new ResponseApi(200, "Alquileres encontrados", alquileres.Select(a => MapToDTO(a)).ToList());
            }
            else { return new ResponseApi(404, "No se encontraron alquileres registrados"); }
            
        }

        public async Task<ResponseApi> GetById(int id)
        {
            Alquiler? alquiler = await _repo.GetById(id);
            if (alquiler != null)
            {
                return new ResponseApi(200, "Alquiler encontrado", MapToDTO(alquiler));
            }
            else { return new ResponseApi(404, "Alquiler no encontrado"); }
        }

        private async Task<string> Validaciones(ModifyAlquilerDTO ma)
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

                }

                // fecha fin no debe ser anterior a la fecha de incio
                if (ma.fecha_fin < ma.fecha_inicio)
                {

                }

                //monto no debe ser menor a 0 
                if (ma.monto < 0)
                {

                }

                // sucursal debe existir 
                if (await _repoSucursal.GetById(ma.id_sucursal) == null)
                {

                }

                //vehiculo debe existir
                if (await _repoVehiculos.GetById(ma.id_vehiculo) == null)
                {

                }

                //en caso de que el idcliente sea mayor a 0 ver que exista (como en idcliente y estado) 

                if (await _repoClientes.GetById(ma.id_clienteNavigation.id_cliente) == null && ma.id_clienteNavigation.id_cliente > 0)
                {

                }

                //

                return "";

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<ResponseApi> Post(ModifyAlquilerDTO ma)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApi> Put(ModifyAlquilerDTO ma)
        {
            throw new NotImplementedException();
        }
    }
}
