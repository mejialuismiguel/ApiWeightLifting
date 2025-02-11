using System.Data;
using Microsoft.Data.SqlClient;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Implementación del servicio de países que maneja las operaciones para obtener la lista de países
    public class CountryService : ICountryService
    {
        private readonly string _connectionString;

        // Constructor que inicializa la cadena de conexión a la base de datos
        public CountryService(IConfiguration configuration)
        {
            // Obtiene la cadena de conexión desde la configuración
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Método para obtener la lista de países desde la base de datos con paginación y filtro opcional por nombre
        public async Task<IEnumerable<Country>> GetCountries(int pageNumber, int pageSize, string? name = null)
        {
            var countries = new List<Country>();

            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_getCountries'
                using (SqlCommand cmd = new SqlCommand("sp_getCountries", conn))
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
                            var country = new Country
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Code = reader.GetString(2)
                            };
                            // Agrega el país a la lista de países
                            countries.Add(country);
                        }
                    }
                }
            }

            // Retorna la lista de países
            return countries;
        }

        // Metodo para agregar paises
        public async Task AddCountry(Country country)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_addcountry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", country.Name);
                    command.Parameters.AddWithValue("@Code", country.Code);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}