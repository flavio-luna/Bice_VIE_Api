using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Entities
{
    public class NotificacionEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CodigoInidicador { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioEntity Usuario { get; set; }

        [ForeignKey("AlertaId")]
        public AlertaEntity Alerta { get; set; }

    }
}
