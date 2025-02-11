using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Resumen de los intentos de un atleta en un torneo")]
    public class AthleteAttemptSummary
    {
        [SwaggerSchema("Nombre del atleta")]
        public required string AthleteName { get; set; }

        [SwaggerSchema("Documento Nacional de Identidad del atleta")]
        public required string AthleteDni { get; set; }

        [SwaggerSchema("Nombre del torneo")]
        public required string TournamentName { get; set; }

        [SwaggerSchema("Identificador único del torneo")]
        public int TournamentId { get; set; }

        [SwaggerSchema("Número total de intentos realizados por el atleta en el torneo")]
        public int TotalAttempts { get; set; }
    }
}