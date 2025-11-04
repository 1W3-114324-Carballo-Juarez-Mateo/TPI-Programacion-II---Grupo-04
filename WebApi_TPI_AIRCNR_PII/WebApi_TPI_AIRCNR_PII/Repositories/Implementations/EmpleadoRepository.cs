using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class EmpleadoRepository : IEmpleadoRepository
    {

        private readonly AlquileresContext _context;
        private readonly DbSet<Empleado> _empleados;

        public EmpleadoRepository(AlquileresContext context)
        {
            _context = context;
            _empleados = _context.Set<Empleado>();
        }

        public async Task<Empleado?> GetByLegajo(int legajo)
        {
            return await _empleados.AsNoTracking().FirstOrDefaultAsync(e => e.legajo == legajo);
        }

        public async Task<Empleado?> GetByUser(int idUser)
        {
            return await _empleados.AsNoTracking()
                .Include(e => e.id_sucursalNavigation)
                .Include(e => e.id_tipo_documentoNavigation)
                .Include(e => e.id_usuarioNavigation)
                .FirstOrDefaultAsync(e => e.id_usuario == idUser);
        }

        public async Task<bool> Post(Empleado e)
        {
            await _empleados.AddAsync(e);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
