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
        private readonly IAuxiliarRepository<Tipos_Vehiculo> _tipoVehiculo;
        private readonly IAuxiliarRepository<Marca> _marca;
        private readonly IAuxiliarRepository<Estados_Vehiculo> _estadoVehiculo;
        public VehiculoService(IVehiculoRepository repo, IAuxiliarRepository<Tipos_Vehiculo> tipoVehiculo)
        {
            _repo = repo;
            _tipoVehiculo = tipoVehiculo;
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
                id_sucursalNavigation = v.id_sucursalNavigation
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
            List<Vehiculo>? vehiculos = await _repo.GetAll();
            if (vehiculos != null && vehiculos.Any())
            {
                return new ResponseApi(200, "Vehiculos encontrados", vehiculos.Select(v => MapToDTO(v)).ToList());
            }
            else { return new ResponseApi(404, "No se encontraron vehiculos disponibles"); }
        }

        public async Task<ResponseApi> GetByPatent(string patente)
        {
            Vehiculo? v = await _repo.GetByPatent(patente);
            if (v != null)
            {
                return new ResponseApi(200, "Vehiculo encontrado", MapToDTO(v));
            }
            else { return new ResponseApi(404, "Vehiculo no encontrado"); }
        }



        private async Task<int> Validaciones(ModifyVehiculoDTO v)
        {
            //Id_Vehiculo
            if (await _repo.GetById(v.id_vehiculo) != null)
            {
                return 1;
            };

            //Patente 2
            //si es put, debe EXISTIR 
            //si es post NO DEBE EXISTIR 
            //Tenemos id.
            // en put: id existe ? si patenete ? si if(patente existe y id existe )= modificar
            //post id existe? no, patente? no if(insert)


            //Marca que exista 3

            //Tipo_Vehiculo /que exista 4
            if()
            
            //Modelo /que exista 5
            
            //Valor_Tasado / que no sea menor de 1Millon 6
            
            //Estado_Vehiculo / que exista 7
            
            //Sucursal /que exista 8

            return 0;
        }

        public Task<ResponseApi> Post(ModifyVehiculoDTO v)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApi> Put(ModifyVehiculoDTO v)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApi> SoftDelete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseApi> GetById(int id)
        {
            Vehiculo? v = await _repo.GetById(id);
            if (v != null)
            {
                return new ResponseApi(200, "Vehiculo encontrado", MapToDTO(v));
            }
            else { return new ResponseApi(404, "Vehiculo no encontrado"); }
        }
    }
}
