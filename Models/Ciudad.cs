using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models
{
    public class Ciudad
    {
        public int CiudadId { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Name { get; set; }
        public int TecnicoId { get; internal set; }
    }
}
