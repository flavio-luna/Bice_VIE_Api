using BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos;
using System;
using System.Collections.Generic;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos
{
    public class IndicadorEconomicoDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Unidad_Medida { get; set; }
        public DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public List<Serie> Valores { get; set; }

        public IndicadorEconomicoDto()
        {
            Valores = new List<Serie>();
        }
    }
}
