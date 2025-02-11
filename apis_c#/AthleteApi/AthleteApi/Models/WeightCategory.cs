using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa una categoría de peso en el sistema")]
    public class WeightCategory
    {
        [SwaggerSchema("Identificador único de la categoría de peso")]
        public int Id { get; set; }

        [SwaggerSchema("Nombre de la categoría de peso")]
        public string Name { get; set; } = string.Empty;

        [SwaggerSchema("Peso mínimo de la categoría")]
        public double MinWeight { get; set; }

        [SwaggerSchema("Peso máximo de la categoría")]
        public double MaxWeight { get; set; }

        [SwaggerSchema("Género asociado a la categoría de peso")]
        public string Gender { get; set; } = string.Empty;
    }
}