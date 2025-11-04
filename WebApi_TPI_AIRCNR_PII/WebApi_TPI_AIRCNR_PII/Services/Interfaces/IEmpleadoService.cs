using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.Services.Interfaces
{
    public interface IEmpleadoService
    {
        public Task<EmpleadoDTO?> GetByUser(int idUser);
        public Task<EmpleadoDTO?> GetByLegajo(int legajo);
        public Task<ResponseApi> Post(EmpleadoDTO e);
    }
}
