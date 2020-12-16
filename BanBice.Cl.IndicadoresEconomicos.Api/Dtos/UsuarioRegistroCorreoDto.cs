using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos
{
    public class UsuarioRegistroCorreoDto
    {
       
        public Guid IdValidacion { get; set; }

        public string Contrasenna { get; set; }

        public KeyValuePair<bool, string> EsValido()
        {
            var respuesta = new KeyValuePair<bool, string>(true, "Es Valida");
            return respuesta;
        }
    }
}
