using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Implementations;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IJwtService _service;
        private readonly IEmpleadoService _empleadoService;

        public AuthController(IJwtService service, IEmpleadoService empleadoService)
        {
            _service = service;
            _empleadoService = empleadoService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuario)
        {
            var response = await _service.ValidateLogin(usuario);
            return StatusCode(response.code, response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] EmpleadoDTO empleado)
        {
            var response = await _empleadoService.Post(empleado);
            return StatusCode(response.code, response);
        }
    }
}
