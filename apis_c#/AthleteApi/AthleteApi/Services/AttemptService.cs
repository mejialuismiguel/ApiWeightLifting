using System.Data;
using Microsoft.Data.SqlClient;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Implementación del servicio de intentos que maneja las operaciones CRUD para los intentos
    public class AttemptService : IAttemptService
    {
        private readonly string _connectionString;

        // Constructor que inicializa la cadena de conexión a la base de datos
        public AttemptService(IConfiguration configuration)
        {
            // Obtiene la cadena de conexión desde la configuración
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null");
        }

        // Método para agregar un nuevo intento en la base de datos
        public async Task AddAttempt(Attempt attempt)
        {
            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_addattempt'
                using (SqlCommand cmd = new SqlCommand("sp_addattempt", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@ParticipationId", attempt.ParticipationId);
                    cmd.Parameters.AddWithValue("@AttemptNumber", attempt.AttemptNumber);
                    cmd.Parameters.AddWithValue("@Type", attempt.Type);
                    cmd.Parameters.AddWithValue("@WeightLifted", attempt.WeightLifted);
                    cmd.Parameters.AddWithValue("@Success", attempt.Success);

                    // Abre la conexión a la base de datos
                    conn.Open();
                    // Ejecuta el comando de forma asíncrona
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para obtener una lista de intentos por torneo desde la base de datos con paginación
        public async Task<IEnumerable<Attempt>> GetAttemptsByTournament(int? tournamentId, string tournamentName, int pageNumber, int pageSize)
        {
            var attempts = new List<Attempt>();

            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_getattemptsbytournament'
                using (SqlCommand cmd = new SqlCommand("sp_getattemptsbytournament", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@TournamentId", tournamentId.HasValue ? (object)tournamentId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TournamentName", tournamentName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    // Abre la conexión a la base de datos
                    conn.Open();
                    // Ejecuta el comando y obtiene un lector de datos de forma asíncrona
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee los datos devueltos por el lector de datos
                        while (await reader.ReadAsync())
                        {
                            var attempt = new Attempt
                            {
                                Id = reader.GetInt32(0),
                                ParticipationId = reader.GetInt32(1),
                                AttemptNumber = reader.GetInt32(2),
                                Type = reader.GetString(3),
                                WeightLifted = reader.GetDouble(4),
                                Success = reader.GetBoolean(5) ? 1 : 0,
                                TournamentName = reader.GetString(6),
                                TournamentId = reader.GetInt32(7)
                            };
                            // Agrega el intento a la lista de intentos
                            attempts.Add(attempt);
                        }
                    }
                }
            }

            // Retorna la lista de intentos
            return attempts;
        }
    }
}