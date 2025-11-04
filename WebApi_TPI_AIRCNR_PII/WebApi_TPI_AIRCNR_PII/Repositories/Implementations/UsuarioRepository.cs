using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly AlquileresContext _context;
        private readonly DbSet<Usuarios_Empleado> _usuarios;

        public UsuarioRepository(AlquileresContext context)
        {
            _context = context;
            _usuarios = _context.Set<Usuarios_Empleado>();
        }

        public async Task<Usuarios_Empleado?> GetByUsername(string username)
        {
            return await _usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.usuario == username);
        }
    }
}
