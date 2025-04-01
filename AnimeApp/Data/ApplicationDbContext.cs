using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnimeApp.Models;

namespace AnimeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Director> Directores { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Pelicula>()
                .HasOne(p => p.Director)
                .WithMany()
                .HasForeignKey(p => p.DirectorId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Pelicula>()
                .HasOne(p => p.Genero)
                .WithMany()
                .HasForeignKey(p => p.GeneroId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Pelicula>()
                .HasOne(p => p.Pais)
                .WithMany()
                .HasForeignKey(p => p.PaisId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Director>()
                .HasOne(d => d.Pais)
                .WithMany()
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Actor>()
                .HasOne(a => a.Pais)
                .WithMany()
                .HasForeignKey(a => a.PaisId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

    }
}
