using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }
        public string? Nombre { get; set; }
        public double sueldoHora { get; set; }
    }

}
