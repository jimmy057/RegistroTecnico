using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models
{
    public class Tickets
    {
        [Key]
        public int TicketsId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        [StringLength(10, ErrorMessage = "La prioridad debe tener un máximo de 10 caracteres.")]
        public string Prioridad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Cliente ID es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El Cliente ID debe ser un número positivo.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El asunto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El asunto debe tener un máximo de 100 caracteres.")]
        public string Asunto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MinLength(10, ErrorMessage = "La descripción debe tener al menos 10 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo invertido debe ser un número positivo.")]
        public decimal TiempoInvertido { get; set; } = 0;

        [Required(ErrorMessage = "El Técnico ID es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El Técnico ID debe ser un número positivo.")]
        public int TecnicoId { get; set; }
    }
}

