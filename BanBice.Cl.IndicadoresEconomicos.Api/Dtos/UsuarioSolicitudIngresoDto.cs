using System.Collections.Generic;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos
{
    public class UsuarioSolicitudIngresoDto
    {

        public string Correo { get; set; }

        public string Contrasenna { get; set; }

        public KeyValuePair<bool, string> EsValido()
        {
            var respuesta = new KeyValuePair<bool, string>(true, "Es Valida");
            return respuesta;
        }

    }
}
