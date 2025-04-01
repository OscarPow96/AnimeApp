using System.ComponentModel.DataAnnotations;

namespace AnimeApp.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La reseña es obligatoria")]
        public string Resena { get; set; }

        public string ImagenPortadaUrl { get; set; }
        public string CodigoTrailer { get; set; }

        // Relaciones con otras tablas
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }

        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public List<Actor> Actores { get; set; } = new List<Actor>();
    }
}
