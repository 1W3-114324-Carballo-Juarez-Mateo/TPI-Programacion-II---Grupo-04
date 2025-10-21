using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Interfaces
{
    public interface IAlquilerRepository
    {
        public Task<List<Alquiler>?> GetAll(string docCliente, string? estado);
        public Task<Alquiler?> GetById(int id);
        public Task<bool> Post(Alquiler a);
        public Task<bool> Put(Alquiler a);
        public Task<bool> CambioEstado(int id, string estado);
    }
}
