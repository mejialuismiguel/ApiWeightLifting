using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa un país en el sistema")]
    public class Country
    {
        [SwaggerSchema("Identificador único del país")]
        public int Id { get; set; }

        [SwaggerSchema("Nombre del país")]
        public required string Name { get; set; }

        [SwaggerSchema("Código del país")]
        public required string Code { get; set; }
    }
}