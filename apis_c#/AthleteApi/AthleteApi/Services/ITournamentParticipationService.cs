using System.Collections.Generic;
using System.Threading.Tasks;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de participación en torneos
    public interface ITournamentParticipationService
    {
        // Método para agregar un nuevo participante en un torneo
        Task AddParticipant(TournamentParticipation participation);

        // Método para obtener una lista de participantes con paginación y filtro opcional por torneo y nombre del atleta
        Task<IEnumerable<TournamentParticipation>> GetParticipants(int? tournamentId, string? athleteName, int pageNumber, int pageSize);
    }
}
