using BanBice.Cl.IndicadoresEconomicos.Api.Dtos;
using BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos;
using BanBice.Cl.IndicadoresEconomicos.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Helpers
{
    public static class ToDtoEntensions
    {
        public static FavoritoDto ToDto(this FavoritoEntity favoritoEntity)
        {
            return Mapper.Mappear<FavoritoDto>(favoritoEntity);   
        }

        public static List<FavoritoDto> ToListDto(this List<FavoritoEntity> favoritoEntities) 
        {
            return favoritoEntities.Select(f => f.ToDto()).ToList();
        }


        public static UsuarioDto ToDto(this UsuarioEntity usuarioEntity) 
        {
            return Mapper.Mappear<UsuarioDto>(usuarioEntity);
        }

        public static AlertaDto ToDto(this AlertaEntity alertaEntity) 
        {
            return Mapper.Mappear<AlertaDto>(alertaEntity);
        }


        public static List<AlertaDto> ToListDto(this List<AlertaEntity> alertas) 
        {
            return alertas.Select(a => a.ToDto()).ToList();
        }

        public static List<IndicadorEconomicoDto> ToListDto(this RootTodos todos)
        {
            var listaIndicadores = new List<IndicadorEconomicoDto>();
            listaIndicadores.Add(todos.bitcoin.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.uf.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.ivp.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.dolar.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.dolar_intercambio.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.euro.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.ipc.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.utm.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.imacec.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.tpm.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.libra_cobre.Mappear<IndicadorEconomicoDto>());
            listaIndicadores.Add(todos.tasa_desempleo.Mappear<IndicadorEconomicoDto>());
            return listaIndicadores;
        }


        public static IndicadorEconomicoDto ToDto(this RootIndicadorPeriodo periodo)
        {
            return new IndicadorEconomicoDto
            {
                Valores = periodo.serie,
                Nombre = periodo.nombre,
                Codigo = periodo.codigo,
                Unidad_Medida = periodo.unidad_medida
            };
        }
    }
}
