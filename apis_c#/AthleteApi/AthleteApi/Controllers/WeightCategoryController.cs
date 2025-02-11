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
    public class WeightCategoryController : ControllerBase
    {
        private readonly IWeightCategoryService _weightCategoryService;
        private readonly ILogger<WeightCategoryController> _logger;

        // Constructor que inicializa el servicio de categorías de peso y el logger
        public WeightCategoryController(IWeightCategoryService weightCategoryService, ILogger<WeightCategoryController> logger)
        {
            _weightCategoryService = weightCategoryService;
            _logger = logger;
        }

        // Método HTTP GET para obtener la lista de categorías de peso con paginación
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene la lista de categorías de peso",
            Description = "Obtiene la lista de categorías de peso con paginación. " +
                          "La paginación se controla con los parámetros `pageNumber` y `pageSize`."
        )]
        [SwaggerResponse(200, "Lista de categorías de peso obtenida exitosamente", typeof(IEnumerable<WeightCategory>))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> GetWeightCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Llama al servicio para obtener la lista de categorías de peso
                var weightCategories = await _weightCategoryService.GetWeightCategories(pageNumber, pageSize);
                // Retorna la lista de categorías de peso obtenida en la respuesta HTTP con estado 200 OK
                return Ok(weightCategories);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener las categorías de peso");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }

        // Método HTTP POST para agregar una nueva categoría de peso
        [HttpPost]
        [SwaggerOperation(
            Summary = "Agrega una nueva categoría de peso",
            Description = "Agrega una nueva categoría de peso al sistema. Los campos requeridos son:\n" +
                          "- `Name`: Nombre de la categoría de peso.\n" +
                          "- `MinWeight`: Peso mínimo de la categoría.\n" +
                          "- `MaxWeight`: Peso máximo de la categoría.\n" +
                          "- `Gender`: Género asociado a la categoría de peso."
        )]
        [SwaggerResponse(200, "Categoría de peso agregada satisfactoriamente", typeof(ApiResponse))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> AddWeightCategory([FromBody] WeightCategory weightCategory)
        {
            try
            {
                // Llama al servicio para agregar una nueva categoría de peso
                await _weightCategoryService.AddWeightCategory(weightCategory);
                // Retorna una respuesta HTTP con estado 200 OK y un mensaje de éxito
                return Ok(new ApiResponse("Categoría de peso agregada satisfactoriamente", 0));
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al agregar la categoría de peso");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}