using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Interfaces
{
    public interface IVehiculoRepository
    {
        public Task<List<Vehiculo>?> GetAll();

        public Task<Vehiculo?> GetByPatent(string patente);

        public Task<bool> Post(Vehiculo v);

        public Task<bool> Put(Vehiculo v);

        public Task<bool> SoftDelete(int id);
        
        public Task<bool> CambioEstado(int id, string nvoEstado);
    }
}
