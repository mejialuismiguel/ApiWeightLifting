using System;
using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa un torneo de levantamiento de pesas")]
    public class Tournament
    {
        [SwaggerSchema("Identificador único del torneo")]
        public int Id { get; set; }

        [SwaggerSchema("Nombre del torneo")]
        public required string Name { get; set; }

        [SwaggerSchema("Ubicación del torneo")]
        public required string Location { get; set; }

        [SwaggerSchema("Fecha de inicio del torneo")]
        public required DateTime StartDate { get; set; }

        [SwaggerSchema("Fecha de finalización del torneo")]
        public required DateTime EndDate { get; set; }
    }
}