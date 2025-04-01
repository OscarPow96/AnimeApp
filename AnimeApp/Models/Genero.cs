using System.ComponentModel.DataAnnotations;

namespace AnimeApp.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del género es obligatorio")]
        public string Nombre { get; set; }
    }
}
