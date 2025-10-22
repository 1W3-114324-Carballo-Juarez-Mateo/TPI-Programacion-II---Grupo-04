using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        public Task<List<Cliente>?> GetAll();
        public Task<Cliente?> GetById(int id);
        public Task<Cliente?> GetByDocument(string documento);
    }
}
