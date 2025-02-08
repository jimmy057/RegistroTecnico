using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Sistemas
    {
        [Key]
        public int SistemasId { get; set; }
       
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public String Descripcion { get; set; }

        [Range(1, 100, ErrorMessage = "La complejidad debe estar entre 1 y 100.")]
        public int Complejidad { get; set; }
        public int TecnicoId { get; set; }
    }
}
