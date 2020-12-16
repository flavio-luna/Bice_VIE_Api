using BanBice.Cl.IndicadoresEconomicos.Api.Dtos;
using BanBice.Cl.IndicadoresEconomicos.Api.Entities;
using BanBice.Cl.IndicadoresEconomicos.Api.Helpers;
using BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IndicadoresEconomicosContext _ctx;
        public UsuarioRepository(IndicadoresEconomicosContext context)
        {
            _ctx = context;
        }

        public async Task<UsuarioDto> AgregarUsuario(UsuarioRegistroDto usuarioDto)
        {

            var usuarioEntity = usuarioDto.ToEntity();
            usuarioEntity.Id = Guid.NewGuid();
            usuarioEntity.IdValidacionCorreo = Guid.NewGuid();
            usuarioEntity.CorreoValidado = false;
            usuarioEntity.FechaEnvioValidacion = DateTime.Now;
            usuarioEntity.HashContrasenna = "";
            usuarioEntity.SaltContrasenna = "";
            usuarioEntity.Bloqueado = false;
            usuarioEntity.Intentos = 0;
            _ctx.Usuarios.Add(usuarioEntity);
            await _ctx.SaveChangesAsync();
            await EnviarNotificacionRegistro(usuarioEntity);
            return usuarioEntity.ToDto();
        }

        public async Task<bool> ExisteCorreo(string correo)
        {
            var existeCorreo = _ctx.Usuarios.Where(u => u.Correo.ToLower().Equals(correo.ToLower())).ToList().Any();
            return existeCorreo;
        }

        public async Task<UsuarioEntity> ObtenerUsuarioPorCorreo(string correo)
        {
            var usuarioEntity = _ctx.Usuarios.FirstOrDefault(u => u.Correo.ToLower().Equals(correo.ToLower()));
            return usuarioEntity;
        }

        public async Task<UsuarioDto> ObtenerUsuarioPorId(Guid idUsuario)
        {
            var usuarioEntity = _ctx.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            return usuarioEntity.ToDto();
        }

        public async Task<UsuarioEntity> ObtenerUsuarioPorIdValidacionCorreo(Guid idValidacionCorreo)
        {
            var usuarioEntity = _ctx.Usuarios.FirstOrDefault(u => u.IdValidacionCorreo == idValidacionCorreo);
            return usuarioEntity;
        }

        public async Task<bool> RegistrarCorreo(UsuarioRegistroCorreoDto usuarioRegistroCorreoDto)
        {
            var usuarioEntity = _ctx.Usuarios.FirstOrDefault(u => u.IdValidacionCorreo == usuarioRegistroCorreoDto.IdValidacion);
            usuarioEntity.SaltContrasenna = Crypto.CrearSalt(AppVariables.LargoSalt);
            usuarioEntity.HashContrasenna = Crypto.CrearHashSHA256(usuarioRegistroCorreoDto.Contrasenna, usuarioEntity.SaltContrasenna);
            usuarioEntity.CorreoValidado = true;
            usuarioEntity.Bloqueado = false;
            usuarioEntity.Intentos = 0;
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReenviarValidacionCorreo(Guid idUsuario) 
        {
            var usuarioEntity = _ctx.Usuarios.First(u => u.Id == idUsuario);
            usuarioEntity.FechaEnvioValidacion = DateTime.Now;
            usuarioEntity.CorreoValidado = false;
            usuarioEntity.Bloqueado = false;
            usuarioEntity.Intentos = 0;
            await _ctx.SaveChangesAsync();
            await EnviarNotificacionRegistro(usuarioEntity);
            return true;
        }



        public async Task<bool> AumentarIntentoUsuario(Guid idUsuario)
        {
            var bloqueado = false;
            var usuarioEntity = _ctx.Usuarios.First(u => u.Id == idUsuario);
            usuarioEntity.Intentos++;
            if (usuarioEntity.Intentos > AppVariables.IntentosParaBloqueoUsuario) {
                usuarioEntity.Bloqueado = true;
                bloqueado = true;
            }
            await _ctx.SaveChangesAsync();
            return bloqueado;
        }

        private async Task<bool> EnviarNotificacionRegistro(UsuarioEntity usuarioEntity) 
        {
            return true;
        }
    }
}
