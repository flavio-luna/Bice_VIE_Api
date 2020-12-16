using BanBice.Cl.IndicadoresEconomicos.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Repositories.Interfaces
{
    public interface IAlertaRepository
    {
        Task<AlertaDto> AgregarAlerta(AlertaDto favoritoDto);

        Task<AlertaDto> EditarAlerta(AlertaDto favoritoDto);

        Task<bool> BorrarAlerta(Guid alertaId);

        Task<IEnumerable<AlertaDto>> ObtenerAlertasPorUsuarioId(Guid usuarioId);
    }
}
