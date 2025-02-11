using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AthleteApi.Models;
using AthleteApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        // Constructor que inicializa el servicio de países y el logger
        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        // Método HTTP GET para obtener la lista de países con paginación y filtro opcional por nombre
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene la lista de países",
            Description = "Obtiene la lista de países con paginación y filtro opcional por nombre. " +
                          "Los filtros disponibles son:\n" +
                          "- `name`: Filtra por el nombre del país.\n" +
                          "La paginación se controla con los parámetros `pageNumber` y `pageSize`."
        )]
        [SwaggerResponse(200, "Lista de países obtenida exitosamente", typeof(IEnumerable<Country>))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> GetCountries(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
        {
            try
            {
                // Llama al servicio para obtener la lista de países
                var countries = await _countryService.GetCountries(pageNumber, pageSize, name);
                // Retorna la lista de países obtenida en la respuesta HTTP con estado 200 OK
                return Ok(countries);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener los países");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }

        // Método HTTP POST para agregar un nuevo país
        [HttpPost]
        [SwaggerOperation(
            Summary = "Agrega un nuevo país",
            Description = "Agrega un nuevo país al sistema. Los campos requeridos son:\n" +
                          "- `Name`: Nombre del país.\n" +
                          "- `Code`: Código del país."
        )]
        [SwaggerResponse(200, "País agregado satisfactoriamente", typeof(ApiResponse))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> AddCountry([FromBody] Country country)
        {
            try
            {
                // Llama al servicio para agregar un nuevo país
                await _countryService.AddCountry(country);
                // Retorna una respuesta HTTP con estado 200 OK y un mensaje de éxito
                return Ok(new ApiResponse("País agregado satisfactoriamente", 0));
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al agregar el país");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}