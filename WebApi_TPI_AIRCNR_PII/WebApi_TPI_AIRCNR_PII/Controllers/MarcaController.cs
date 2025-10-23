using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Implementations;
using WebApi_TPI_AIRCNR_PII.Services.Implementations;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        private readonly IAuxiliarService<Marca> _service;

        public MarcaController(IAuxiliarService<Marca> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var respuesta = await _service.GetAll();
            return StatusCode(respuesta.code, respuesta);
        }

        //Tipos de Peticiones HTTP
        [HttpPost]
        [HttpPut]
        [HttpDelete]
        [HttpGet("{id}")]

        //Tipos de Parametros
        [FromRoute]
        [FromQuery]
        [FromBody]
    }
}
