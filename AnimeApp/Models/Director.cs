using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeApp.Models
{
    public class Director
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [Display(Name = "Pais")]
        public int PaisId { get; set; }
        [ForeignKey("PaisId")]
        public Pais Pais { get; set; }
    }
}
