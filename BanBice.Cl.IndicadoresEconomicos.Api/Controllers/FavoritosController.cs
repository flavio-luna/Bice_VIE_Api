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
    public class FavoritosController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFavoritoRepository _favoritoRepository;

        public FavoritosController(ILogger<FavoritosController> logger, IFavoritoRepository favoritoRepository)
        {
            _logger = logger;
            _favoritoRepository = favoritoRepository;
        }

        //agregar
        [HttpPost("agregar-favorito")]
        public async Task<IActionResult> AgregarFavorito([FromBody] FavoritoDto favoritoDto) 
        {
            try
            {
                favoritoDto.UsuarioId = JwtHelper.ObtenerIdUsuario(User.Claims);
                favoritoDto =  await _favoritoRepository.AgregarFavorito(favoritoDto);
                return Ok(favoritoDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fallo en Post Favorito: {ex}");
                return BadRequest();
            }
        }

        //quitar
        [HttpDelete("{favoritoId}", Name = "borrar-favorito")]
        public async Task<IActionResult> BorrarFavorito(Guid favoritoId) 
        {
            try
            {
                var borrado = await _favoritoRepository.BorrarFavorito(favoritoId);
                if (!borrado )
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"fallo en Post Favorito: {ex}");
                return BadRequest();
            }
        }


        //favoritosDeUsuario
        [HttpGet("favoritos-de-usuario")]        
        public async Task<IActionResult> FavoritosDeUsuario() 
        {
            try
            {
                var idUsuario = JwtHelper.ObtenerIdUsuario(User.Claims);
                var favoritosDto = await _favoritoRepository.ObtenerFavoritosPorUsuarioId(idUsuario);
                return Ok(favoritosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error en Favoritos de usuario: {ex}");
                return BadRequest();
            }    
        }

    }
}
