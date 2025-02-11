using System.Collections.Generic;
using System.Threading.Tasks;
using AthleteApi.Models;

namespace AthleteApi.Services
{
    // Interfaz que define los métodos para el servicio de países
    public interface ICountryService
    {
        // Método para obtener la lista de países con paginación y filtro opcional por nombre
        Task<IEnumerable<Country>> GetCountries(int pageNumber, int pageSize, string? name = null);
        // Método para agregar un nuevo país
        Task AddCountry(Country country);
    }
}