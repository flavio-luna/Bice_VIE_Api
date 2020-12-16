using System;
using System.ComponentModel.DataAnnotations;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Entities
{
    public class UsuarioEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Correo { get; set; }
       
        [Required]
        [MaxLength(200)]
        public string NombreCompleto { get; set; }

        [MaxLength(15)]
        public string Celular { get; set; }

        public bool EnvioNewsletter { get; set; }

        [Required]
        public int Edad { get; set; }

        public bool CorreoValidado { get; set; }

        public Guid IdValidacionCorreo { get; set; }

        public DateTime FechaEnvioValidacion { set; get; }

        [MaxLength(500)]
        public string SaltContrasenna { get; set; }

        [MaxLength(500)]
        public string HashContrasenna { get; set; }

        public int Intentos { get; set; }

        public bool Bloqueado { get; set; }



    }
}
