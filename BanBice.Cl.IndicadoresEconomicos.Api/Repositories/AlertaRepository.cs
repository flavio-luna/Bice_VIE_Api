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
    public class AlertaRepository : IAlertaRepository
    {
        private IndicadoresEconomicosContext _ctx;
        public AlertaRepository(IndicadoresEconomicosContext context)
        {
            _ctx = context;
        }

        public async Task<AlertaDto> AgregarAlerta(AlertaDto alertaDto)
        {
            var alertaEntity = alertaDto.ToEntity();
            alertaEntity.Id = Guid.NewGuid();
            _ctx.Alertas.Add(alertaEntity);
            await _ctx.SaveChangesAsync();
            return alertaEntity.ToDto();
        }

        public async Task<bool> BorrarAlerta(Guid alertaId)
        {
            var alertaEntity = _ctx.Alertas.FirstOrDefault(a => a.Id == alertaId);
            if (alertaEntity == null) {
                return false;
            }
            _ctx.Alertas.Remove(alertaEntity);
            await _ctx.SaveChangesAsync();
            return true;            
        }

        public async Task<AlertaDto> EditarAlerta(AlertaDto alertaDto)
        {
            var alertaEntity = _ctx.Alertas.FirstOrDefault(a => a.Id == alertaDto.Id);
            alertaEntity.Nombre = alertaDto.Nombre;
            alertaEntity.DentroDelTramo = alertaDto.DentroDelTramo;
            alertaEntity.Desde = alertaDto.Desde;
            alertaEntity.Hasta = alertaDto.Hasta;
            await _ctx.SaveChangesAsync();
            return alertaDto;
        }

        public async Task<IEnumerable<AlertaDto>> ObtenerAlertasPorUsuarioId(Guid usuarioId)
        {
            var alertasEntities = _ctx.Alertas.Where(a => a.UsuarioId == usuarioId).ToList();
            return alertasEntities.ToListDto();
        }

    }
}
