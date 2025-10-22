using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {

        private readonly AlquileresContext _context;
        private readonly DbSet<Cliente> _clientes;

        public ClienteRepository(AlquileresContext context)
        {
            _context = context;
            _clientes = _context.Set<Cliente>();
        }

        public async Task<List<Cliente>?> GetAll()
        {
            return await _clientes.AsNoTracking().ToListAsync();
        }

        public async Task<Cliente?> GetByDocument(string documento)
        {
            return await _clientes.AsNoTracking().FirstOrDefaultAsync(c => c.documento == documento);          
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _clientes.AsNoTracking().FirstOrDefaultAsync(c => c.id_cliente == id);
        }
    }
}
