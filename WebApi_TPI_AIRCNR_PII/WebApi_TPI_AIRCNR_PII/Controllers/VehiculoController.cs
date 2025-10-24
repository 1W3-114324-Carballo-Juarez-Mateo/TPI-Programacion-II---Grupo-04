using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _service;

        public VehiculoController(IVehiculoService service)
        {
            _service = service;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CambioEstado([FromRoute] int id, [FromQuery] string nvoEstado)
        {
            var respuesta = await _service.CambioEstado(id, nvoEstado);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var respuesta = await _service.GetAll();
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpGet("Vehiculo/patente/{patente}")]
        public async Task<IActionResult> GetByPatent([FromRoute] string patente)
        {
            var respuesta = await _service.GetByPatent(patente);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var respuesta = await _service.GetById(id);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ModifyVehiculoDTO vehiculo)
        {
            var respuesta = await _service.Post(vehiculo);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ModifyVehiculoDTO vehiculo)
        {
            var respuesta = await _service.Put(vehiculo);
            return StatusCode(respuesta.code, respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var respuesta = await _service.SoftDelete(id);
            return StatusCode(respuesta.code, respuesta);
        }
    }
}
