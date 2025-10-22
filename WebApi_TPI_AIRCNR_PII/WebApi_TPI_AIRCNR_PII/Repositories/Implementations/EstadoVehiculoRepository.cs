using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class EstadoVehiculoRepository : IAuxiliarRepository<Estados_Vehiculo>
    {

        private readonly AlquileresContext _context;
        private readonly DbSet<Estados_Vehiculo> _estados;

        public EstadoVehiculoRepository(AlquileresContext context)
        {
            _context = context;
            _estados = _context.Set<Estados_Vehiculo>();
        }

        public async Task<List<Estados_Vehiculo>?> GetAll()
        {
            try
            {
                return await _estados.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }

        public async Task<Estados_Vehiculo?> GetById(int id)
        {
            try
            {
                return await _estados.AsNoTracking().FirstOrDefaultAsync(e => e.id_estado_vehiculo == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }
    }
}
