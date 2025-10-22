using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.Services.Interfaces
{
    public interface IVehiculoService
    {
        public Task<ResponseApi> GetAll();

        public Task<ResponseApi> GetById(int id);

        public Task<ResponseApi> GetByPatent(string patente);

        public Task<ResponseApi> Post(ModifyVehiculoDTO v);

        public Task<ResponseApi> Put(ModifyVehiculoDTO v);

        public Task<ResponseApi> SoftDelete(int id);

        public Task<ResponseApi> CambioEstado(int id, string nvoEstado);
    }
}
