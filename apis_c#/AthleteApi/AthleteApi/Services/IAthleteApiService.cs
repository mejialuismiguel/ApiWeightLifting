using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de atletas
    public interface IAthleteService
    {
        // Método para crear un nuevo atleta
        Task CreateAthlete(Athlete athlete);

        // Método para obtener una lista de atletas con paginación y filtro opcional por nombre
        Task<IEnumerable<Athlete>> GetAthletes(
            int pageNumber, int pageSize, string? name = null);
    }
}