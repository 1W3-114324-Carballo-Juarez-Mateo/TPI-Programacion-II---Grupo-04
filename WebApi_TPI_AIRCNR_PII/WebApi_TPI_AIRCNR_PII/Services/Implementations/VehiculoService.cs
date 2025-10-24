using System.Net.Http.Headers;
using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class VehiculoService : IVehiculoService
    {

        private readonly IVehiculoRepository _repo;
        private readonly IAuxiliarRepository<Tipos_Vehiculo> _repoTipoVehiculo;
        private readonly IAuxiliarRepository<Marca> _repoMarcas;
        private readonly IAuxiliarRepository<Estados_Vehiculo> _repoEstadoVehiculo;
        private readonly IAuxiliarRepository<Sucursal> _repoSucursal;
        public VehiculoService(IVehiculoRepository repo, IAuxiliarRepository<Tipos_Vehiculo> repoTipoVehiculo,
            IAuxiliarRepository<Marca> repoMarcas, IAuxiliarRepository<Estados_Vehiculo> repoEstadoVehiculo,
            IAuxiliarRepository<Sucursal> repoSucursal)
        {
            _repo = repo;
            _repoTipoVehiculo = repoTipoVehiculo;
            _repoMarcas = repoMarcas;
            _repoEstadoVehiculo = repoEstadoVehiculo;
            _repoSucursal = repoSucursal;
        }

        private Vehiculo MapToBD(ModifyVehiculoDTO v)
        {
            return new Vehiculo
            {
                id_vehiculo = v.id_vehiculo,
                patente = v.patente,
                id_tipo_vehiculo = v.id_tipo_vehiculo,
                id_marca = v.id_marca,
                modelo = v.modelo,
                valor_tasado = v.valor_tasado,
                id_estado_vehiculo = v.id_estado_vehiculo,
                id_sucursal = v.id_sucursal
            };
        } 

        private VehiculoDTO MapToDTO(Vehiculo v)
        {
            return new VehiculoDTO
            {
                id_vehiculo = v.id_vehiculo,
                patente = v.patente,
                modelo = v.modelo,
                valor_tasado = v.valor_tasado,
                id_tipo_vehiculoNavigation = v.id_tipo_vehiculoNavigation,
                id_marcaNavigation = v.id_marcaNavigation,
                id_estado_vehiculoNavigation = v.id_estado_vehiculoNavigation,
                id_sucursalNavigation = v.id_sucursalNavigation,
            };   
        }

        public async Task<ResponseApi> CambioEstado(int id, string nvoEstado)
        {
            try
            {
                if (await _repo.GetById(id) != null)
                {
                    if (await _repo.CambioEstado(id, nvoEstado))
                    {
                        return new ResponseApi(200, "Estado de vehiculo modificado con exito");
                    }
                    else { return new ResponseApi(400, "No se pudo modificar el estado del vehiculo"); }
                }
                else { return new ResponseApi(404, "Vehiculo no encontrado"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        public async Task<ResponseApi> GetAll()
        {
            try
            {
                List<Vehiculo>? vehiculos = await _repo.GetAll();
                if (vehiculos != null && vehiculos.Any())
                {
                    return new ResponseApi(200, "Vehiculos encontrados", vehiculos.Select(v => MapToDTO(v)).ToList());
                }
                else { return new ResponseApi(404, "No se encontraron vehiculos disponibles"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        public async Task<ResponseApi> GetByPatent(string patente)
        {
            try
            {
                Vehiculo? v = await _repo.GetByPatent(patente);
                if (v != null)
                {
                    return new ResponseApi(200, "Vehiculo encontrado", MapToDTO(v));
                }
                else { return new ResponseApi(404, "Vehiculo no encontrado"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        private async Task<string?> Validaciones(ModifyVehiculoDTO v)
        {
            try
            {
                //Id_Vehiculo
                if (await _repo.GetById(v.id_vehiculo) == null && v.id_vehiculo > 0)
                {
                    return "No se pudo encontrar el vehiculo";
                }

                //Patente que exista 2
                List<Vehiculo>? vehiculos = await _repo.GetAll();
                if (vehiculos != null && vehiculos.Any())
                {
                    if (vehiculos.Any(ve => ve.patente == v.patente && ve.id_vehiculo != v.id_vehiculo))
                    {
                        return "Ya existe un vehiculo con esa patente";
                    }
                }

                //Marca que exista 3           
                if (await _repoMarcas.GetById(v.id_marca) == null) 
                {
                    return "La marca ingresada no se encuentra registrada";                
                }

                //Tipo_Vehiculo /que exista 4
                if (await _repoTipoVehiculo.GetById(v.id_tipo_vehiculo) == null) 
                {
                    return "El tipo de vehiculo no es valido";                
                }

                //Modelo /que exista 5
                if(string.IsNullOrEmpty(v.modelo))               
                {
                    return "Debe ingresar un modelo de vehiculo";                
                }      
                
                 //Valor_Tasado / que no sea menor de 1Millon 6
                if (v.valor_tasado <= 2500000) 
                {
                    return "El valor tasado debe ser mayor a $2.500.000";                
                }

                //Valor_Tasado no debe ser mayor a $100.000.000
                if (v.valor_tasado > 100000000)
                {
                    return "El valor tasado no debe superar la cifra de $100.000.000";
                }

                //Sucursal /que exista 8
                if (await _repoSucursal.GetById(v.id_sucursal) == null) 
                {
                    return "No existe la sucursal ingresada";
                }

                 return null;
            }
            catch (Exception)
            {
                return "Error interno en validaciones";
            }
        }

        public async Task<ResponseApi> Post(ModifyVehiculoDTO v)
        {
            string? validacion = await Validaciones(v);
            v.id_vehiculo = 0;
            try
            {
                if (string.IsNullOrEmpty(validacion))
                {
                    if (await _repo.Post(MapToBD(v)))
                    {
                        return new ResponseApi(200, "Vehiculo agregado con exito", v);
                    }
                    else { return new ResponseApi(400, "No se pudo agregar el vehiculo", v); }
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

        public async Task<ResponseApi> Put(ModifyVehiculoDTO v)
        {
            string? validacion = await Validaciones(v);
            try
            {
                if (string.IsNullOrEmpty(validacion))
                {
                    if (await _repo.Put(MapToBD(v)))
                    {
                        return new ResponseApi(200, "El vehiculo fue modificado con exito", v);
                    }
                    else { return new ResponseApi(400, "No se pudo modificar el vehiculo", v); }
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

        public async Task<ResponseApi> SoftDelete(int id)
        {
            try
            {
                Vehiculo? v = await _repo.GetById(id);

                if (v != null)
                {
                    if (v.id_estado_vehiculo != 35)
                    {
                        if (await _repo.SoftDelete(id))
                        {
                            return new ResponseApi(200, "Vehiculo dado de baja con exito");
                        }
                        else { return new ResponseApi(400, "No se pudo dar de baja el vehiculo"); }
                    }
                    else { return new ResponseApi(400, "El vehiculo seleccionado ya fue dado de baja"); }
                }
                else { return new ResponseApi(404, "Vehiculo no encontrado"); }
            }
            catch(Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        public async Task<ResponseApi> GetById(int id)
        {
            try
            {
                Vehiculo? v = await _repo.GetById(id);
                if (v != null)
                {
                    return new ResponseApi(200, "Vehiculo encontrado", MapToDTO(v));
                }
                else { return new ResponseApi(404, "Vehiculo no encontrado"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }
    }
}
