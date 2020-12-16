using System;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos.Inidicadores
{
    public class IndicadorBase
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string unidad_medida { get; set; }
        public DateTime fecha { get; set; }
        public double valor { get; set; }
    }
}
