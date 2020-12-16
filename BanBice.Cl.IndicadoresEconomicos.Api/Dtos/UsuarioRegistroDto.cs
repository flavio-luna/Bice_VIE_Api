using System.Collections.Generic;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos
{
    public class UsuarioRegistroDto
    {
        
        public string NombreCompleto { get; set; }       
        
        public string Correo { get; set; }

        public string Celular { get; set; }

        public bool EnvioNewsletter { get; set; }

        public int Edad { get; set; }

        public KeyValuePair<bool, string> EsValido()
        {
            var respuesta = new KeyValuePair<bool, string>(true, "Es Valida");
            return respuesta;
        }
    }
}
