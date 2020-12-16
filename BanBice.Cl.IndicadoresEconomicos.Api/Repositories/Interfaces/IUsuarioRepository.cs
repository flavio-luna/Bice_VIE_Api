using BanBice.Cl.IndicadoresEconomicos.Api.Dtos;
using BanBice.Cl.IndicadoresEconomicos.Api.Entities;
using System;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioDto> ObtenerUsuarioPorId(Guid idUsuario);

        Task<bool> ExisteCorreo(string correo);

        Task<UsuarioDto> AgregarUsuario(UsuarioRegistroDto usuarioDto);

        Task<bool> RegistrarCorreo(UsuarioRegistroCorreoDto usuarioRegistroCorreoDto);

        Task<UsuarioEntity> ObtenerUsuarioPorCorreo(string correo);
        Task<UsuarioEntity> ObtenerUsuarioPorIdValidacionCorreo(Guid idValidacionCorreo);

        Task<bool> AumentarIntentoUsuario(Guid idUsuario);

        Task<bool> ReenviarValidacionCorreo(Guid idUsuario);



    }
}
