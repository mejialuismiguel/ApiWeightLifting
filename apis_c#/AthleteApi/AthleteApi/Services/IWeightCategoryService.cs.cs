using System.Collections.Generic;
using System.Threading.Tasks;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de categorías de peso
    public interface IWeightCategoryService
    {
        // Método para obtener una lista de categorías de peso con paginación
        Task<IEnumerable<WeightCategory>> GetWeightCategories(int pageNumber, int pageSize);
        // Método para crear una nueva categoría de peso
        Task AddWeightCategory(WeightCategory weightCategory);
    }
}