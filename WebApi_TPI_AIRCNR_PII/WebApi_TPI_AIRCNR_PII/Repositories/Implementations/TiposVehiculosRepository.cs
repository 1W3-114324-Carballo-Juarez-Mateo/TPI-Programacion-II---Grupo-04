using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class TiposVehiculosRepository : IAuxiliarRepository<Tipos_Vehiculo>
    {
        private readonly AlquileresContext _context;
        private readonly DbSet<Tipos_Vehiculo> _tiposVehiculos;

        public TiposVehiculosRepository(AlquileresContext context)
        {
            _context = context;
            _tiposVehiculos = _context.Set<Tipos_Vehiculo>();
        }
        public async Task<List<Tipos_Vehiculo>?> GetAll()
        {
            try
            {
                return await _tiposVehiculos
                .AsNoTracking()
                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }
    }
}
