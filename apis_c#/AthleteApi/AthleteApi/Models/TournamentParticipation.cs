using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa la participación de un atleta en un torneo")]
    public class TournamentParticipation
    {
        [SwaggerSchema("Identificador único de la participación")]
        public int Id { get; set; }

        [SwaggerSchema("Identificador del atleta")]
        public int AthleteId { get; set; }

        [SwaggerSchema("Identificador del torneo")]
        public int TournamentId { get; set; }

        [SwaggerSchema("Identificador de la categoría de peso")]
        public int WeightCategoryId { get; set; }
    }
}