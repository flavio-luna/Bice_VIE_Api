using System;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos
{
    public class FavoritoDto
    {
  
        public Guid Id { get; set; }
   
        public string CodigoInidicador { get; set; }        

        public Guid UsuarioId { get; set; }
    }
}
