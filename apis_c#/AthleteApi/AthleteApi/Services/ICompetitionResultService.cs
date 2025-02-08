using System.Collections.Generic;
using System.Threading.Tasks;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de resultados de competencias
    public interface ICompetitionResultService
    {
        // Método para obtener los resultados de la competencia con paginación y filtro opcional por torneo
        Task<IEnumerable<CompetitionResult>> GetCompetitionResults(int? tournamentId, string? tournamentName, int pageNumber, int pageSize);
    }
}