using BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos;
using System;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces
{
    public interface IIndicadorEconomicoRepository
    {
        Task<RootTodos> GetTodos();

        Task<RootIndicadorPeriodo> GetIndicadoresPorTipo(string tipo);

        Task<RootIndicadorPeriodo> GetIndicadoresPorTipoYFecha(string tipo, DateTime fecha);

        Task<RootIndicadorPeriodo> GetIndicadoresPorTipoYAnno(string tipo, string anno);
    }
}
