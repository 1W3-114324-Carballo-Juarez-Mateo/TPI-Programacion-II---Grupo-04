using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class TiposVehiculoService : IAuxiliarService<Tipos_Vehiculo>
    {
        private readonly IAuxiliarRepository<Tipos_Vehiculo> _repo;

        public TiposVehiculoService(IAuxiliarRepository<Tipos_Vehiculo> repo)
        {
            _repo = repo;
        }

        public async Task<ResponseApi> GetAll()
        {

            try
            {
                List<Tipos_Vehiculo>? Tv = await _repo.GetAll();
                if (Tv != null && Tv.Any())
                {
                    return new ResponseApi(200, "Tipos de vehiculos", Tv.Select(v => new TipoVehiculoDTO { id_tipo_vehiculo = v.id_tipo_vehiculo, tipo = v.tipo }));
                }
                else { return new ResponseApi(404, "No se encontraron tipos de vehiculos"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }
    }
}
