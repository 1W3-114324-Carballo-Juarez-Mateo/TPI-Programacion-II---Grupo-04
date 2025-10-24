using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoVehiculoController : ControllerBase
    {

        private readonly IAuxiliarService<Tipos_Vehiculo> _service;

        public TipoVehiculoController(IAuxiliarService<Tipos_Vehiculo> service)
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
