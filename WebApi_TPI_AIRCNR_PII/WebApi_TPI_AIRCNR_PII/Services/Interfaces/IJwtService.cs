using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;

namespace WebApi_TPI_AIRCNR_PII.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username);

        Task<ResponseApi> ValidateLogin(UsuarioDTO usuario);
    }
}
