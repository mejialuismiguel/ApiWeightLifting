using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa un intento de levantamiento de pesas en un torneo")]
    public class Attempt
    {
        [SwaggerSchema("Identificador único del intento")]
        public int Id { get; set; }

        [SwaggerSchema("Identificador de la participación del atleta en el torneo")]
        public int ParticipationId { get; set; }

        [SwaggerSchema("Número del intento")]
        public int AttemptNumber { get; set; }

        [SwaggerSchema("Tipo de intento (ej. Snatch, Clean and Jerk)")]
        public string? Type { get; set; }

        [SwaggerSchema("Peso levantado en el intento")]
        public double WeightLifted { get; set; }

        [SwaggerSchema("Indica si el intento fue exitoso (1) o fallido (0)")]
        public int Success { get; set; }

        [SwaggerSchema("Nombre del torneo")]
        public string? TournamentName { get; set; }

        [SwaggerSchema("Identificador único del torneo")]
        public int TournamentId { get; set; }
    }
}