using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombres { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Direccion { get; set; }

        [StringLength(11, ErrorMessage = "El RNC no puede exceder los 11 caracteres.")]
        public string RNc { get; set; }

        [Required(ErrorMessage = "El límite de crédito es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LimiteCredito { get; set; }

        // Relación con Técnico
        [Required(ErrorMessage = "El Técnico asociado es obligatorio.")]
        public int TecnicoId { get; set; }
    }
}
