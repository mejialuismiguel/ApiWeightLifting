using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa un atleta en el sistema")]
    public class Athlete
    {
        [SwaggerSchema("Identificador único del atleta")]
        public int Id { get; set; } = 0;

        [Required]
        [SwaggerSchema("Documento Nacional de Identidad del atleta")]
        public string Dni { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("Nombre del atleta")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("Apellido del atleta")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("Fecha de nacimiento del atleta")]
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        [Required]
        [SwaggerSchema("Género del atleta")]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("Identificador del país del atleta")]
        public int CountryId { get; set; } = 0;

        [Required]
        [SwaggerSchema("Identificador de la categoría de peso del atleta")]
        public int WeightCategoryId { get; set; } = 0;
    }
}