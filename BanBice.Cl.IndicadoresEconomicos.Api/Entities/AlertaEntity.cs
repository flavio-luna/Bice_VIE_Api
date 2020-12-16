using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BanBice.Cl.IndicadoresEconomicos.Api.Entities
{
    public class AlertaEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CodigoInidicador { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioEntity Usuario { get; set;}

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }


        public Guid UsuarioId { get; set; }

        [Required]
        public bool DentroDelTramo { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Desde { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Hasta { get; set; }
    }
}
