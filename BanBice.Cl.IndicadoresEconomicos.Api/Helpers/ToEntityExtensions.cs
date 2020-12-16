using BanBice.Cl.IndicadoresEconomicos.Api.Dtos;
using BanBice.Cl.IndicadoresEconomicos.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Helpers
{
    public static class ToEntityExtensions
    {
        public static FavoritoEntity ToEntity(this FavoritoDto favoritoDto) 
        {
            return Mapper.Mappear<FavoritoEntity>(favoritoDto);
        }

        public static UsuarioEntity ToEntity(this UsuarioRegistroDto usuarioRegistroDto) 
        {
            return Mapper.Mappear<UsuarioEntity>(usuarioRegistroDto);
        }

        public static AlertaEntity ToEntity(this AlertaDto alertaDto) 
        {
            return Mapper.Mappear<AlertaEntity>(alertaDto);
        }
    }
}
