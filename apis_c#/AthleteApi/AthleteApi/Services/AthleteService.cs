using System.Data;
using Microsoft.Data.SqlClient;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Implementación del servicio de atletas que maneja las operaciones CRUD para los atletas
    public class AthleteService : IAthleteService
    {
        private readonly string _connectionString;

        // Constructor que inicializa la cadena de conexión a la base de datos
        public AthleteService(IConfiguration configuration)
        {
            // Obtiene la cadena de conexión desde la configuración
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Método para crear un nuevo atleta en la base de datos
        public async Task CreateAthlete(Athlete athlete)
        {
            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_createathlete'
                using (SqlCommand cmd = new SqlCommand("sp_createathlete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@p_dni", athlete.Dni);
                    cmd.Parameters.AddWithValue("@p_first_name", athlete.FirstName);
                    cmd.Parameters.AddWithValue("@p_last_name", athlete.LastName);
                    cmd.Parameters.AddWithValue("@p_birth_date", athlete.BirthDate);
                    cmd.Parameters.AddWithValue("@p_gender", athlete.Gender);
                    cmd.Parameters.AddWithValue("@p_country_id", athlete.CountryId);
                    cmd.Parameters.AddWithValue("@p_weight_category_id", athlete.WeightCategoryId);

                    // Abre la conexión a la base de datos
                    conn.Open();
                    // Ejecuta el comando de forma asíncrona
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para obtener una lista de atletas desde la base de datos con paginación y filtro opcional por nombre
        public async Task<IEnumerable<Athlete>> GetAthletes(
            int pageNumber, int pageSize, string? name = null)
        {
            var athletes = new List<Athlete>();

            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_getathletes'
                using (SqlCommand cmd = new SqlCommand("sp_getathletes", conn))
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
                            var athlete = new Athlete
                            {
                                Id = reader.GetInt32(0),
                                Dni = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                LastName = reader.GetString(3),
                                BirthDate = reader.GetDateTime(4),
                                Gender = reader.GetString(5),
                                CountryId = reader.GetInt32(6),
                                WeightCategoryId = reader.GetInt32(7)
                            };
                            // Agrega el atleta a la lista de atletas
                            athletes.Add(athlete);
                        }
                    }
                }
            }

            // Retorna la lista de atletas
            return athletes;
        }
    }
}