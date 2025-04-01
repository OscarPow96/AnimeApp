using System.ComponentModel.DataAnnotations;

namespace AnimeApp.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        public int PaisId { get; set; }
        public Pais Pais { get; set; }
    }
}
