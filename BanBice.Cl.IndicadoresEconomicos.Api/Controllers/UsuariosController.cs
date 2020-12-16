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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger _logger;
        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioRepository usuario)
        {
            _usuarioRepository = usuario;
            _logger = logger;
        }


        //validarCorreo, registrar, registrarCorreo, solicitarIngreso, usuarioActual
        [HttpGet("usuario-actual")]
        [Authorize]
        public async Task<IActionResult> UsuarioActual() {
            var respuesta = new RespuestaApiDto();
            try
            {

                var idUsuario = JwtHelper.ObtenerIdUsuario(User.Claims);
                if (idUsuario == null)
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Usuario no esta logueado";                    
                }
                else 
                {
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Usuario logueado";
                    respuesta.ObjetoRespuesta = idUsuario;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fallo en Get usuario actual: {ex}");
                respuesta.Codigo = 500;
                respuesta.Mensaje = "Excepción, consultar log.";
            }
            return Ok(respuesta);
        }


        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDto usuarioRegistroDto) 
        {
            var respuesta = new RespuestaApiDto();
            try
            {
                var usuarioValido = usuarioRegistroDto.EsValido();
                if (!usuarioValido.Key) 
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = usuarioValido.Value;
                }
                else
                {
                    var existeCorreo = await _usuarioRepository.ExisteCorreo(usuarioRegistroDto.Correo);

                    if (existeCorreo)
                    {
                        respuesta.Codigo = -1;
                        respuesta.Mensaje = "Correo existe en base de datos";
                    }
                    else
                    {
                        respuesta.Codigo = 1;
                        respuesta.Mensaje = "Registro exitoso";
                        var usuarioDto = await _usuarioRepository.AgregarUsuario(usuarioRegistroDto);
                        respuesta.ObjetoRespuesta = usuarioDto;
                    }
                }                          
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fallo en Post Registrar Usuario: {ex}");
                respuesta.Codigo = 500;
                respuesta.Mensaje = "Excepción, consultar log.";
            }
            return Ok(respuesta);
        }


        [HttpPost("registrar-correo")]
        public async Task<IActionResult> RegistrarCorreo(UsuarioRegistroCorreoDto usuarioRegistroCorreoDto)
        {
            var respuesta = new RespuestaApiDto();
            try
            {
                var usuarioEntity = await _usuarioRepository.ObtenerUsuarioPorIdValidacionCorreo(usuarioRegistroCorreoDto.IdValidacion);
                if (usuarioEntity == null) 
                {
                    return BadRequest();
                }

                if (usuarioEntity.FechaEnvioValidacion.AddDays(AppVariables.DiasPermitidosParaValidarCorreo).Subtract(DateTime.Now).TotalSeconds < 0)
                {
                    await _usuarioRepository.ReenviarValidacionCorreo(usuarioEntity.Id);
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "Reenvio de Correo Validación";
                }
                else 
                {
                    await _usuarioRepository.RegistrarCorreo(usuarioRegistroCorreoDto);
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "Registro exitoso";
                    respuesta.ObjetoRespuesta = usuarioEntity.ToDto();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fallo en Post Registrar Correo: {ex}");
                respuesta.Codigo = 500;
                respuesta.Mensaje = "Excepción, consultar log.";
            }
            return Ok(respuesta);
        }


        [HttpPost("solicitar-ingreso")]
        public async Task<IActionResult> SolicitarIngreso([FromBody] UsuarioSolicitudIngresoDto usuarioSolicitudIngresoDto)
        {
            var respuesta = new RespuestaApiDto();
            try
            {
                var usuarioValido = usuarioSolicitudIngresoDto.EsValido();
                if (!usuarioValido.Key)
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = usuarioValido.Value;
                }
                else
                {
                    var usuarioEntity = await _usuarioRepository.ObtenerUsuarioPorCorreo(usuarioSolicitudIngresoDto.Correo);
                    if (usuarioEntity == null)
                    {
                        respuesta.Codigo = -1;
                        respuesta.Mensaje = "usuario y/o contraseña equivocados";
                    }
                    else 
                    {
                        var hashPass = Crypto.CrearHashSHA256(usuarioSolicitudIngresoDto.Contrasenna, usuarioEntity.SaltContrasenna);

                        if (usuarioEntity.Bloqueado == true)
                        {
                            respuesta.Codigo = -2;
                            respuesta.Mensaje = "usuario bloqueado";
                        }
                        else if (usuarioEntity.CorreoValidado == false)
                        {
                            respuesta.Codigo = -3;
                            respuesta.Mensaje = "usuario sin correo validado";
                        }
                        else if (!usuarioEntity.HashContrasenna.Equals(hashPass))
                        {
                            respuesta.Codigo = -4;
                            respuesta.Mensaje = "usuario y/o contraseña equivocados";
                            var bloqueado = await _usuarioRepository.AumentarIntentoUsuario(usuarioEntity.Id);
                        }
                        else 
                        {
                            respuesta.Codigo = 1;
                            respuesta.Mensaje = "usuario valido";
                            respuesta.ObjetoRespuesta = JwtHelper.GenerarToken(usuarioEntity);
                        }
                        //respuesta.ObjetoRespuesta = usuarioEntity.ToDto();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fallo en Post Solicitar Ingreso: {ex}");
                respuesta.Codigo = 500;
                respuesta.Mensaje = "Excepción, consultar log.";
            }
            return Ok(respuesta);
        }



        [HttpPost("{correo}", Name = "solicitar-reestablecer-contrasenna")]
        public async Task<IActionResult> SolicitarReestablecerContrasenna(string correo)
        {
            var respuesta = new RespuestaApiDto();
            try
            {
                var usuarioDto = await _usuarioRepository.ObtenerUsuarioPorCorreo(correo);

                if (usuarioDto == null)
                {
                    respuesta.Codigo = -1;
                    respuesta.Mensaje = "No se pudo solicitar reestablecer";
                }
                else
                {
                    await _usuarioRepository.ReenviarValidacionCorreo(usuarioDto.Id);
                    respuesta.Codigo = 1;
                    respuesta.Mensaje = "se ha enviado correo";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"fallo en Solicitar Reestablecer Contrasenna: {ex}");
                respuesta.Codigo = 500;
                respuesta.Mensaje = "Excepción, consultar log.";
            }
            return Ok(respuesta);
        }


    }
}
