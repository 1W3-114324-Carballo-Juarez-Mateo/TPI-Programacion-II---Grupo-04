namespace WebApi_TPI_AIRCNR_PII.Repositories.Interfaces
{
    public interface IAuxiliarRepository <T>
    {
        public Task<List<T>?> GetAll();

        public Task<T?> GetById(int id);
    }
}
