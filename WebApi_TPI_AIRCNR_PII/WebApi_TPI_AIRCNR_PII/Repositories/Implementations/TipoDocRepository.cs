using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class TipoDocRepository : IAuxiliarRepository<Tipos_Documento>
    {   
        private readonly AlquileresContext _context;
        private readonly DbSet<Tipos_Documento> _documentos;

        public TipoDocRepository(AlquileresContext context)
        {
            _context = context;
            _documentos = _context.Set<Tipos_Documento>();
        }

        public async Task<List<Tipos_Documento>?> GetAll()
        {
            try
            {
                return await _documentos
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
