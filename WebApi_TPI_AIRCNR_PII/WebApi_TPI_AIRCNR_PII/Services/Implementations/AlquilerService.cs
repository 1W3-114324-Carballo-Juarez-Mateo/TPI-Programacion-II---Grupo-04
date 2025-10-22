using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class AlquilerService : IAlquilerService
    {
        public Task<ResponseApi> CambioEstado(int id, string estado)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApi> GetAll(string docCliente, string? estado)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApi> GetById(int id)
        {
            throw new NotImplementedException();
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
