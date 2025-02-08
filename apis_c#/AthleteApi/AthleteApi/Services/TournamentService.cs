using System.Data;
using Microsoft.Data.SqlClient;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Implementación del servicio de torneos que maneja las operaciones CRUD para los torneos
    public class TournamentService : ITournamentService
    {
        private readonly string _connectionString;

        // Constructor que inicializa la cadena de conexión a la base de datos
        public TournamentService(IConfiguration configuration)
        {
            // Obtiene la cadena de conexión desde la configuración
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Método para crear un nuevo torneo en la base de datos
        public async Task CreateTournament(Tournament tournament)
        {
            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_createTournament'
                using (SqlCommand cmd = new SqlCommand("sp_createTournament", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Name", tournament.Name);
                    cmd.Parameters.AddWithValue("@Location", tournament.Location);
                    cmd.Parameters.AddWithValue("@StartDate", tournament.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", tournament.EndDate);

                    // Abre la conexión a la base de datos
                    conn.Open();
                    // Ejecuta el comando de forma asíncrona
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para obtener una lista de torneos desde la base de datos con paginación y filtro opcional por nombre
        public async Task<IEnumerable<Tournament>> GetTournaments(
            int pageNumber, int pageSize, string? name = null)
        {
            var tournaments = new List<Tournament>();

            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_getTournaments'
                using (SqlCommand cmd = new SqlCommand("sp_getTournaments", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@Name", name ?? (object)DBNull.Value);

                    // Abre la conexión a la base de datos
                    conn.Open();
                    // Ejecuta el comando y obtiene un lector de datos de forma asíncrona
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee los datos devueltos por el lector de datos
                        while (await reader.ReadAsync())
                        {
                            var tournament = new Tournament
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Location = reader.GetString(2),
                                StartDate = reader.GetDateTime(3),
                                EndDate = reader.GetDateTime(4)
                            };
                            // Agrega el torneo a la lista de torneos
                            tournaments.Add(tournament);
                        }
                    }
                }
            }

            // Retorna la lista de torneos
            return tournaments;
        }
    }
}