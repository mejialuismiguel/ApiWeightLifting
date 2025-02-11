using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa el resultado de una competencia de levantamiento de pesas")]
    public class CompetitionResult
    {
        [SwaggerSchema("País del atleta")]
        public required string Pais { get; set; }

        [SwaggerSchema("Nombre del atleta")]
        public required string Nombre { get; set; }

        [SwaggerSchema("Peso levantado en la modalidad de arranque")]
        public double Arranque { get; set; }

        [SwaggerSchema("Peso levantado en la modalidad de envión")]
        public double Envion { get; set; }

        [SwaggerSchema("Peso total levantado en la competencia")]
        public double TotalPeso { get; set; }
    }
}