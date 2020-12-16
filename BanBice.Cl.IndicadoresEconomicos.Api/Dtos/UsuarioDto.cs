using System;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Dtos
{
    public class UsuarioDto
    {
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public Guid Id { get; set; }

    }
}
