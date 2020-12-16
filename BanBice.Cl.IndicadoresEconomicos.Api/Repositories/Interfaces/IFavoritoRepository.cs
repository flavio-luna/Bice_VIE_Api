using BanBice.Cl.IndicadoresEconomicos.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces
{
    public interface IFavoritoRepository
    {
        Task<FavoritoDto> AgregarFavorito(FavoritoDto favoritoDto);

        Task<bool> BorrarFavorito(Guid favoritoId);

        Task<IEnumerable<FavoritoDto>> ObtenerFavoritosPorUsuarioId(Guid usuarioId);
    }
}
