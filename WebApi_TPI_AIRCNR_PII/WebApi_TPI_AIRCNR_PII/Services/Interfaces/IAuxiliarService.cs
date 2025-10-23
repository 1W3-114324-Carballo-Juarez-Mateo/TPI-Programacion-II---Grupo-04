using WebApi_TPI_AIRCNR_PII.Helper;

namespace WebApi_TPI_AIRCNR_PII.Services.Interfaces
{
    public interface IAuxiliarService<T>
    {
        public Task<ResponseApi> GetAll();
    }
}
