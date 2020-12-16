using System;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos
{
    public class AlertaDto
    {
        public Guid Id { get; set; }
     
        public string CodigoInidicador { get; set; }
        
        public string Nombre { get; set; }

        public Guid UsuarioId { get; set; }
      
        public bool DentroDelTramo { get; set; }

        public decimal Desde { get; set; }

        public decimal Hasta { get; set; }
    }
}
