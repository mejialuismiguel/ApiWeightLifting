using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AthleteApi.Models;
using AthleteApi.Services;

namespace AthleteApi.Controllers
{
    // Controlador que maneja las solicitudes relacionadas con los atletas
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AthleteController : ControllerBase
    {
        private readonly IAthleteService _athleteService;
        private readonly ILogger<AthleteController> _logger;

        // Constructor que inicializa el servicio de atletas y el logger
        public AthleteController(IAthleteService athleteService, ILogger<AthleteController> logger)
        {
            _athleteService = athleteService;
            _logger = logger;
        }

        // Método HTTP POST para crear un nuevo atleta
        [HttpPost]
        public async Task<IActionResult> CreateAthlete([FromBody] Athlete athlete)
        {
            try
            {
                // Llama al servicio para crear un nuevo atleta
                await _athleteService.CreateAthlete(athlete);
                // Retorna una respuesta HTTP con estado 200 OK y un mensaje de éxito
                return Ok(new ApiResponse("Atleta creado satisfactoriamente", 0));
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al crear el atleta");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }

        // Método HTTP GET para obtener una lista de atletas con paginación y filtro opcional por nombre
        [HttpGet]
        public async Task<IActionResult> GetAthletes(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
        {
            try
            {
                // Llama al servicio para obtener la lista de atletas
                var athletes = await _athleteService.GetAthletes(pageNumber, pageSize, name);
                // Retorna la lista de atletas obtenida en la respuesta HTTP con estado 200 OK
                return Ok(athletes);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener los atletas");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}