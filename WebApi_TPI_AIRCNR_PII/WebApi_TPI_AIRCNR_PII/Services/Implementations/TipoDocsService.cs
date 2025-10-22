using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class TipoDocsService : IAuxiliarService
    {

        private readonly IAuxiliarRepository <Tipos_Documento> _repo;
        public TipoDocsService(IAuxiliarRepository<Tipos_Documento> repo)
        {
            _repo = repo;
        }

       
        public async Task<ResponseApi> GetAll()
        {

            List<Tipos_Documento>? TD = await _repo.GetAll();
            if (TD != null && TD.Any())
            {
                return new ResponseApi(200, "Tipos de documentos", TD.Select(d => new TipoDocDTO { id_tipo_documento = d.id_tipo_documento, tipo = d.tipo }));
            }
            else { return new ResponseApi(404, "No se encontraron tipos de documentos"); }
        }
    }
}

