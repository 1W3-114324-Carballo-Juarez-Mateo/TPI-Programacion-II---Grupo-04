using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocController : ControllerBase
    {

        private readonly IAuxiliarService<Tipos_Documento> _service;

        public TipoDocController(IAuxiliarService<Tipos_Documento> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var respuesta = await _service.GetAll();
            return StatusCode(respuesta.code, respuesta);
        }

    }
}
