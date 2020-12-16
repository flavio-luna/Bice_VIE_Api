using BanBice.Cl.IndicadoresEconomicos.Api.Dtos;
using BanBice.Cl.IndicadoresEconomicos.Api.Helpers;
using BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlertasController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAlertaRepository _alertaRepository;

        public AlertasController(ILogger<AlertasController> logger, IAlertaRepository alertaRepository)
        {
            _logger = logger;
            _alertaRepository = alertaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AlertaDto alertaDto)
        {
            alertaDto.UsuarioId = JwtHelper.ObtenerIdUsuario(User.Claims);
            alertaDto = await _alertaRepository.AgregarAlerta(alertaDto);
            return Ok(alertaDto);
        }


        [HttpDelete("{alertaId}", Name = "borrar-alerta")]
        public async Task<IActionResult> BorrarAlerta(Guid alertaId)
        {
            var borrado = await _alertaRepository.BorrarAlerta(alertaId);
            if (!borrado) return NotFound();
            return NoContent();
        }


        [HttpPut("editar-alerta")]
        public async Task<IActionResult> EditarAlerta([FromBody] AlertaDto alertaDto) 
        {
            alertaDto = await _alertaRepository.EditarAlerta(alertaDto);
            return Ok(alertaDto);
        }

        [HttpGet("alertas-de-usuario")]
        public async Task<IActionResult> AlertasDeUsuario() 
        {
            var idUsuario = JwtHelper.ObtenerIdUsuario(User.Claims);
            var alertasDto = await _alertaRepository.ObtenerAlertasPorUsuarioId(idUsuario);
            return Ok(alertasDto);
        }
    }
}
