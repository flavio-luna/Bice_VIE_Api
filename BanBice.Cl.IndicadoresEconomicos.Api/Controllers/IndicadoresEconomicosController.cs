using System;
using System.Threading.Tasks;
using BanBice.Cl.IndicadoresEconomicos.Api.Helpers;
using BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicadoresEconomicosController : ControllerBase
    {
        private readonly IIndicadorEconomicoRepository _indicadorEconomicoRepository;
        private readonly ILogger _logger;

        public IndicadoresEconomicosController(ILogger<IndicadoresEconomicosController> logger, IIndicadorEconomicoRepository indicadorEconomicoRepository)
        {
            _indicadorEconomicoRepository = indicadorEconomicoRepository;
            _logger = logger;
        }

        [HttpGet("todos")]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                var indicadores = await _indicadorEconomicoRepository.GetTodos();
                return Ok(indicadores.ToListDto());
            }
            catch (Exception ex)
            {
                _logger.LogError($"fallo en GetTodos: {ex}");
                return BadRequest();
            }
        }


        [HttpGet("por-tipo")]
        public async Task<IActionResult> GetIndicadoresPorTipo(string tipo)
        {
            try
            {
                var indicadores = await _indicadorEconomicoRepository.GetIndicadoresPorTipo(tipo);
                return Ok(indicadores.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError($"fallo en GetIndicadoresPorTipo: {ex}");
                return BadRequest();
            }
        }


        [HttpGet("por-tipo-y-fecha")]
        public async Task<IActionResult> GetIndicadoresPorTipoYFecha(string tipo, DateTime fecha)
        {
            try
            {
                var indicadores = await _indicadorEconomicoRepository.GetIndicadoresPorTipoYFecha(tipo, fecha);
                return Ok(indicadores.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError($"fallo en GetIndicadoresPorTipoYFecha: {ex}");
                return BadRequest();
            }
        }

        //4. Consultar por tipo de indicador económico dado un año determinado
        [HttpGet("por-tipo-y-anno")]
        public async Task<IActionResult> GetIndicadoresPorTipoYAnno(string tipo, string anno)
        {
            try
            {
                var indicadores = await _indicadorEconomicoRepository.GetIndicadoresPorTipoYAnno(tipo, anno);
                return Ok(indicadores.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError($"fallo en GetIndicadoresPorTipoYAnno: {ex}");
                return BadRequest();
            }
        }
    }
}
