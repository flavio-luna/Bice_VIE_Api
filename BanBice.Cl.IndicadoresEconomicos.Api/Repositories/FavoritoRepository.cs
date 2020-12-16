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
    public class FavoritoRepository : IFavoritoRepository
    {
        private IndicadoresEconomicosContext _ctx;
        public FavoritoRepository(IndicadoresEconomicosContext context)
        {
            _ctx = context;
        }
        public async Task<FavoritoDto> AgregarFavorito(FavoritoDto favoritoDto)
        {
            var favoritoEntity = favoritoDto.ToEntity();
            favoritoEntity.Id = Guid.NewGuid();
            await _ctx.Favoritos.AddAsync(favoritoEntity);
            await _ctx.SaveChangesAsync();
            return favoritoEntity.ToDto();

        }

        public async Task<bool> BorrarFavorito(Guid favoritoId)
        {
            var favoritoEntity = _ctx.Favoritos.FirstOrDefault(f => f.Id == favoritoId);
            if (favoritoEntity == null) {
                return false;
            }
            _ctx.Favoritos.Remove(favoritoEntity);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FavoritoDto>> ObtenerFavoritosPorUsuarioId(Guid usuarioId)
        {
            var favoritosEntity = _ctx.Favoritos.Where(f => f.UsuarioId == usuarioId).ToList();
            return favoritosEntity.ToListDto();
        }
    }
}
