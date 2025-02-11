using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AthleteApi.Models;
using AthleteApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Controllers
{
    // Controlador que maneja las solicitudes relacionadas con los intentos
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AttemptController : ControllerBase
    {
        private readonly IAttemptService _attemptService;
        private readonly ILogger<AttemptController> _logger;

        // Constructor que inicializa el servicio de intentos y el logger
        public AttemptController(IAttemptService attemptService, ILogger<AttemptController> logger)
        {
            _attemptService = attemptService;
            _logger = logger;
        }

        // Método HTTP POST para agregar un nuevo intento
        [HttpPost]
        [SwaggerOperation(
            Summary = "Agrega un nuevo intento",
            Description = "Agrega un nuevo intento de levantamiento de pesas. Los campos requeridos son:\n" +
                          "- `ParticipationId`: Identificador de la participación del atleta en el torneo.\n" +
                          "- `AttemptNumber`: Número del intento.\n" +
                          "- `Type`: Tipo de intento (ej. Snatch, Clean and Jerk).\n" +
                          "- `WeightLifted`: Peso levantado en el intento.\n" +
                          "- `Success`: Indica si el intento fue exitoso (1) o fallido (0).\n" +
                          "- `TournamentName`: Nombre del torneo.\n" +
                          "- `TournamentId`: Identificador único del torneo."
        )]
        [SwaggerResponse(200, "Intento agregado satisfactoriamente", typeof(ApiResponse))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> AddAttempt([FromBody] Attempt attempt)
        {
            try
            {
                // Llama al servicio para agregar un nuevo intento
                await _attemptService.AddAttempt(attempt);
                // Retorna una respuesta HTTP con estado 200 OK y un mensaje de éxito
                return Ok(new ApiResponse("Intento agregado satisfactoriamente", 0));
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al agregar el intento");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }

        // Método HTTP GET para obtener una lista de intentos por torneo con paginación
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene una lista de intentos por torneo",
            Description = "Obtiene una lista de intentos por torneo con paginación. Los filtros disponibles son:\n" +
                          "- `tournamentId`: Filtra por el identificador del torneo.\n" +
                          "- `tournamentName`: Filtra por el nombre del torneo.\n" +
                          "La paginación se controla con los parámetros `pageNumber` y `pageSize`."
        )]
        [SwaggerResponse(200, "Lista de intentos obtenida exitosamente", typeof(IEnumerable<Attempt>))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> GetAttemptsByTournament(
            [FromQuery] int? tournamentId = null, [FromQuery] string? tournamentName = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Llama al servicio para obtener la lista de intentos por torneo
                var attempts = await _attemptService.GetAttemptsByTournament(tournamentId, tournamentName ?? string.Empty, pageNumber, pageSize);
                // Retorna la lista de intentos obtenida en la respuesta HTTP con estado 200 OK
                return Ok(attempts);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener los intentos");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}