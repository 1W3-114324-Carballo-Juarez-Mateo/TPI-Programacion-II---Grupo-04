using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<Usuarios_Empleado?> GetByUsername(string username);
    }
}
