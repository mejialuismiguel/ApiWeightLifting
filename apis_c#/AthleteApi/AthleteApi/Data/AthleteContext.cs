using Microsoft.EntityFrameworkCore;
using AthleteApi.Models;

namespace AthleteApi.Data
{
    // Contexto de la base de datos para la aplicación de atletas
    public class AthleteContext : DbContext
    {
        // Constructor que inicializa el contexto con las opciones especificadas
        public AthleteContext(DbContextOptions<AthleteContext> options) : base(options) { }

        // DbSet que representa la tabla de atletas en la base de datos
        public DbSet<Athlete> Athletes { get; set; }
        // DbSet que representa la tabla de torneos en la base de datos
        public DbSet<Tournament> Tournaments { get; set; }

        // Método que se llama al crear el modelo para configurar las entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional del modelo para la entidad Athlete
            modelBuilder.Entity<Athlete>(entity =>
            {
                entity.HasKey(e => e.Id); // Define la clave primaria
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50); // Define la propiedad FirstName como requerida y con longitud máxima de 50
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50); // Define la propiedad LastName como requerida y con longitud máxima de 50
                entity.Property(e => e.BirthDate).IsRequired(); // Define la propiedad BirthDate como requerida
                entity.Property(e => e.Gender).IsRequired().HasMaxLength(1); // Define la propiedad Gender como requerida y con longitud máxima de 1
                entity.Property(e => e.CountryId).IsRequired(); // Define la propiedad CountryId como requerida
                entity.Property(e => e.WeightCategoryId).IsRequired(); // Define la propiedad WeightCategoryId como requerida
            });

            // Configuración adicional del modelo para la entidad Tournament
            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Id); // Define la clave primaria
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // Define la propiedad Name como requerida y con longitud máxima de 100
                entity.Property(e => e.Location).IsRequired().HasMaxLength(100); // Define la propiedad Location como requerida y con longitud máxima de 100
                entity.Property(e => e.StartDate).IsRequired(); // Define la propiedad StartDate como requerida
                entity.Property(e => e.EndDate).IsRequired(); // Define la propiedad EndDate como requerida
            });
        }
    }
}