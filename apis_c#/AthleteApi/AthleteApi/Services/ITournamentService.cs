using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de torneos
    public interface ITournamentService
    {
        // Método para crear un nuevo torneo
        Task CreateTournament(Tournament tournament);

        // Método para obtener una lista de torneos con paginación y filtro opcional por nombre
        Task<IEnumerable<Tournament>> GetTournaments(int pageNumber, int pageSize, string? name = null);
    }
}