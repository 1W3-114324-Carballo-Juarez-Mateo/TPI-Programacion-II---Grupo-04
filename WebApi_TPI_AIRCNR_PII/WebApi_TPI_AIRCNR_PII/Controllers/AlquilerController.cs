using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilerController : ControllerBase
    {
        private readonly IAlquilerService _service;

        public AlquilerController(IAlquilerService service)
        {
            _service = service;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CambioEstado([FromRoute] int id, [FromQuery] string estado)
        {
            var respuesta = await _service.CambioEstado(id, estado);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string docCliente, [FromQuery] string? estado)
        {
            var respuesta = await _service.GetAll(docCliente, estado);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var respuesta = await _service.GetById(id);
            return StatusCode(respuesta.code, respuesta);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ModifyAlquilerDTO dto)
        {
            var respuesta = await _service.Post(dto);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ModifyAlquilerDTO dto)
        {
            var respuesta = await _service.Put(dto);
            return StatusCode(respuesta.code, respuesta);
        }
    }
}
