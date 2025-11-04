using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class JwtService : IJwtService
    {

        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IEmpleadoService _empleadoService;
        private readonly JwtSettings _jwtSettings;

        public JwtService(IUsuarioRepository usuarioRepo, IEmpleadoService empleadoService, JwtSettings jwtSettings)
        {
            _usuarioRepo = usuarioRepo;
            _empleadoService = empleadoService;
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(string username)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var IdentificadorUnico = Guid.NewGuid().ToString();

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, IdentificadorUnico),
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };

            var Token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: Claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }

        public async Task<ResponseApi> ValidateLogin(UsuarioDTO usuario)
        {
            Usuarios_Empleado? user = await _usuarioRepo.GetByUsername(usuario.usuario);
            if (user != null)
            {
                var passwordHashed = HashPassword(usuario.contraseña);
                if (user.contraseña == passwordHashed)
                {
                    return new ResponseApi(200, "Logueado Correctamente", new { Token = GenerateToken(usuario.usuario), 
                        Empleado = await _empleadoService.GetByUser(user.id_usuario) });
                }
                else { return new ResponseApi(401, "Contraseña Incorrecta"); }
            }
            else { return new ResponseApi(404, "Nombre de Usuario no Existente"); }
        }
    }
}
