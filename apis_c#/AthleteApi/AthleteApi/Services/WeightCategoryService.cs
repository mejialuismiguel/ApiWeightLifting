using System.Data;
using Microsoft.Data.SqlClient;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Implementación del servicio de categorías de peso que maneja las operaciones CRUD para las categorías de peso
    public class WeightCategoryService : IWeightCategoryService
    {
        private readonly string _connectionString;

        // Constructor que inicializa la cadena de conexión a la base de datos
        public WeightCategoryService(IConfiguration configuration)
        {
            _connectionString = configuration?.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null");
        }

        // Método para obtener una lista de categorías de peso desde la base de datos con paginación
        public async Task<IEnumerable<WeightCategory>> GetWeightCategories(int pageNumber, int pageSize)
        {
            var weightCategories = new List<WeightCategory>();

            // Crea una nueva conexión SQL usando la cadena de conexión
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Crea un nuevo comando SQL para ejecutar el procedimiento almacenado 'sp_getweightcategories'
                using (SqlCommand cmd = new SqlCommand("sp_getweightcategories", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros necesarios para el procedimiento almacenado
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
                            var weightCategory = new WeightCategory
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                MinWeight = reader.GetDouble(2),
                                MaxWeight = reader.GetDouble(3),
                                Gender = reader.GetString(4)
                            };
                            // Agrega la categoría de peso a la lista de categorías de peso
                            weightCategories.Add(weightCategory);
                        }
                    }
                }
            }

            // Retorna la lista de categorías de peso
            return weightCategories;
        }
    }
}