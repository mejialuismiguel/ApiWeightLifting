using System.Collections.Generic;
using System.Threading.Tasks;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de intentos
    public interface IAttemptService
    {
        // Método para agregar un nuevo intento
        Task AddAttempt(Attempt attempt);

        // Método para obtener una lista de intentos por torneo con paginación
        Task<IEnumerable<Attempt>> GetAttemptsByTournament(int? tournamentId, string tournamentName, int pageNumber, int pageSize);
    }
}