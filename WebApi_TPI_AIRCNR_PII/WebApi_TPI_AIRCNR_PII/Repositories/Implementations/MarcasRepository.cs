using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class MarcasRepository : IAuxiliarRepository<Marca>
    {
        private readonly AlquileresContext _context;
        private readonly DbSet<Marca> _marcas;

        public MarcasRepository(AlquileresContext context)
        {
            _context = context;
            _marcas = _context.Set<Marca>();
            
        }
        public async Task<List<Marca>?> GetAll()
        {
            try
            {
                return await _marcas.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }
    }
}
