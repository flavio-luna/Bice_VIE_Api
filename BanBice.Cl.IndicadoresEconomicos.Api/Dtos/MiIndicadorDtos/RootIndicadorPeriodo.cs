
using System.Collections.Generic;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos.MiIndicadorDtos
{
    public class RootIndicadorPeriodo
    {
        public string version { get; set; }
        public string autor { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string unidad_medida { get; set; }
        public List<Serie> serie { get; set; }
    }
}
