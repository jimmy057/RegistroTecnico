using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnico.Models
{
    public class Tickets
    {
        [Key]
        public int TicketsId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public  String Prioridad { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public  String Asunto { get; set; }
        [Required]
        public  string Descripcion { get; set; }
        public Double TiempoInvertido { get; set; }
        [Required]
        public int TecnicoId { get; set; }

    }
}
