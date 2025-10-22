using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;

namespace WebApi_TPI_AIRCNR_PII.Services.Interfaces
{
    public interface IAlquilerService
    {
        public Task<ResponseApi> GetAll(string docCliente, string? estado);
        public Task<ResponseApi> GetById(int id);
        public Task<ResponseApi> Post(ModifyAlquilerDTO ma);
        public Task<ResponseApi> Put(ModifyAlquilerDTO ma);
        public Task<ResponseApi> CambioEstado(int id, string estado);
    }
}
