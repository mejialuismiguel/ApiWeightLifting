using System.Data;
using Microsoft.Data.SqlClient;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Implementación del servicio de participación en torneos que maneja las operaciones CRUD para las participaciones
    public class TournamentParticipationService : ITournamentParticipationService
    {
        private readonly string _connectionString;

        // Constructor que inicializa la cadena de conexión a la base de datos
        public TournamentParticipationService(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null");
        }

        // Método para agregar un nuevo participante en un torneo
        public async Task AddParticipant(TournamentParticipation participation)
        {
            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_addParticipant'
                using (SqlCommand cmd = new SqlCommand("sp_addParticipant", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@AthleteId", participation.AthleteId);
                    cmd.Parameters.AddWithValue("@TournamentId", participation.TournamentId);
                    cmd.Parameters.AddWithValue("@WeightCategoryId", participation.WeightCategoryId);

                    // Abre la conexión a la base de datos
                    conn.Open();
                    // Ejecuta el comando de forma asíncrona
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para obtener una lista de participantes con paginación y filtro opcional por torneo y nombre del atleta
        public async Task<IEnumerable<TournamentParticipation>> GetParticipants(int? tournamentId, string? athleteName, int pageNumber, int pageSize)
        {
            var participants = new List<TournamentParticipation>();

            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_getParticipants'
                using (SqlCommand cmd = new SqlCommand("sp_getParticipants", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@TournamentId", tournamentId.HasValue ? (object)tournamentId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@AthleteName", athleteName ?? (object)DBNull.Value);
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
                            var participation = new TournamentParticipation
                            {
                                Id = reader.GetInt32(0),
                                AthleteId = reader.GetInt32(1),
                                TournamentId = reader.GetInt32(2),
                                WeightCategoryId = reader.GetInt32(3)
                            };
                            // Agrega la participación a la lista de participantes
                            participants.Add(participation);
                        }
                    }
                }
            }

            // Retorna la lista de participantes
            return participants;
        }
    }
}