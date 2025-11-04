using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        private readonly IAuxiliarService<Marca> _service;

        public SecuredController(IAuxiliarService<Marca> service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var respuesta = await _service.GetAll();
            return StatusCode(respuesta.code, respuesta);
        }
    }
}
