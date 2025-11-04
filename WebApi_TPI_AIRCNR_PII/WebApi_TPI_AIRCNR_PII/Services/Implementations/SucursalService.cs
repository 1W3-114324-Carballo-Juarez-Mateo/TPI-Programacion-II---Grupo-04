using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class SucursalService : IAuxiliarService<Sucursal>
    {
        private readonly IAuxiliarRepository<Sucursal> _repo;

        public SucursalService(IAuxiliarRepository<Sucursal> repo)
        {
            _repo = repo;
        }

        public async Task<ResponseApi> GetAll()
        {
            try
            {
                List<Sucursal>? sucursales = await _repo.GetAll();
                if (sucursales != null && sucursales.Any())
                {
                    return new ResponseApi(200, "Sucursales encontradas", sucursales.Select(s => new SucursalDTO { id_sucursal = s.id_sucursal, descripcion = s.descripcion }));
                }
                else { return new ResponseApi(404, "No se encontraron sucursales"); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }
    }
}
