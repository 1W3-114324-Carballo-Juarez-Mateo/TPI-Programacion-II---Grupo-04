using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class MarcasService : IAuxiliarService
    {

        private readonly IAuxiliarRepository<Marca> _repo;

        public MarcasService(IAuxiliarRepository<Marca> repo)
        {
            _repo = repo;
        }

        public async Task<ResponseApi> GetAll()
        {
            List<Marca>? marcas = await _repo.GetAll();
            if (marcas != null && marcas.Any())
            {
                return new ResponseApi(200, "Marcas encontradas", marcas.Select(m => new MarcaDTO { id_marca = m.id_marca, marca = m.marca }));
            }
            else { return new ResponseApi(404, "No se encontraron marcas"); }
        }
    }
}
