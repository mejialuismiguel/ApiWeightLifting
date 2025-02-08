using System.Data;
using Microsoft.Data.SqlClient;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Implementación del servicio de resultados de competencias que maneja las operaciones para obtener resultados
    public class CompetitionResultService : ICompetitionResultService
    {
        private readonly string _connectionString;

        // Constructor que inicializa la cadena de conexión a la base de datos
        public CompetitionResultService(IConfiguration configuration)
        {
            // Obtiene la cadena de conexión desde la configuración
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Método para obtener los resultados de la competencia desde la base de datos con paginación y filtro opcional por torneo
        public async Task<IEnumerable<CompetitionResult>> GetCompetitionResults(int? tournamentId, string? tournamentName, int pageNumber, int pageSize)
        {
            var results = new List<CompetitionResult>();

            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_getCompetitionResults'
                using (SqlCommand cmd = new SqlCommand("sp_getCompetitionResults", conn))
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
                            var result = new CompetitionResult
                            {
                                Pais = reader.GetString(0),
                                Nombre = reader.GetString(1),
                                Arranque = reader.GetDouble(2),
                                Envion = reader.GetDouble(3),
                                TotalPeso = reader.GetDouble(4)
                            };
                            // Agrega el resultado a la lista de resultados
                            results.Add(result);
                        }
                    }
                }
            }

            // Retorna la lista de resultados de la competencia
            return results;
        }
    }
}