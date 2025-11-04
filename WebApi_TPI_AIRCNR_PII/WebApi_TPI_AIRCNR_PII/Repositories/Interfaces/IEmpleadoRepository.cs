using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Interfaces
{
    public interface IEmpleadoRepository
    {
        public Task<Empleado?> GetByUser(int idUser);
        public Task<Empleado?> GetByLegajo(int legajo);
        public Task<bool> Post(Empleado e);
    }
}
