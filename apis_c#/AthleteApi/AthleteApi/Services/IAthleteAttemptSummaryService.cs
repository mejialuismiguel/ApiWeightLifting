using System.Collections.Generic;
using System.Threading.Tasks;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de resúmenes de intentos de atletas
    public interface IAthleteAttemptSummaryService
    {
        // Método para obtener el resumen de intentos de atletas con paginación y filtros opcionales por torneo, atleta, DNI y nombre
        Task<IEnumerable<AthleteAttemptSummary>> GetAthleteAttemptSummary(
            int? tournamentId, int? athleteId, string? athleteDni, string? athleteName, int pageNumber, int pageSize);
    }
}