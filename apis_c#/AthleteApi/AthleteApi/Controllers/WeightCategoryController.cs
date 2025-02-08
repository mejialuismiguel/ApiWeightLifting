using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AthleteApi.Models;
using AthleteApi.Services;

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
    }
}