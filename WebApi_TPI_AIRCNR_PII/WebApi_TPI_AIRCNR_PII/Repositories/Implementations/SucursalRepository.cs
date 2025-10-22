using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class SucursalRepository : IAuxiliarRepository<Sucursal>
    {

        private readonly AlquileresContext _context;
        private readonly DbSet<Sucursal> _sucursales;

        public SucursalRepository(AlquileresContext context)
        {
            _context = context;
            _sucursales = _context.Set<Sucursal>();
        }

        public async Task<List<Sucursal>?> GetAll()
        {
            try
            {
                return await _sucursales.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }

        public async Task<Sucursal?> GetById(int id)
        {
            try
            {
                return await _sucursales.AsNoTracking().FirstOrDefaultAsync(s => s.id_sucursal == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }
    }
}
