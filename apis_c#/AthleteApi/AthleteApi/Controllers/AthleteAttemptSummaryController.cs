using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AthleteApi.Models;
using AthleteApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Controllers
{
    // Controlador que maneja las solicitudes relacionadas con el resumen de intentos de los atletas
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AthleteAttemptSummaryController : ControllerBase
    {
        private readonly IAthleteAttemptSummaryService _athleteAttemptSummaryService;
        private readonly ILogger<AthleteAttemptSummaryController> _logger;

        // Constructor que inicializa el servicio de resúmenes de intentos de atletas y el logger
        public AthleteAttemptSummaryController(IAthleteAttemptSummaryService athleteAttemptSummaryService, ILogger<AthleteAttemptSummaryController> logger)
        {
            _athleteAttemptSummaryService = athleteAttemptSummaryService;
            _logger = logger;
        }

        // Método HTTP GET para obtener el resumen de intentos de los atletas con paginación y filtros opcionales
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene el resumen de intentos de los atletas",
            Description = "Obtiene el resumen de intentos de los atletas con paginación y filtros opcionales. " +
                          "Los filtros disponibles son:\n" +
                          "- `tournamentId`: Filtra por el identificador del torneo.\n" +
                          "- `athleteId`: Filtra por el identificador del atleta.\n" +
                          "- `athleteDni`: Filtra por el Documento Nacional de Identidad del atleta.\n" +
                          "- `athleteName`: Filtra por el nombre del atleta.\n" +
                          "La paginación se controla con los parámetros `pageNumber` y `pageSize`."
        )]        [SwaggerResponse(200, "Resumen de intentos de los atletas obtenido exitosamente", typeof(IEnumerable<AthleteAttemptSummary>))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> GetAthleteAttemptSummary([FromQuery] int? tournamentId = null, [FromQuery] int? athleteId = null, [FromQuery] string? athleteDni = null, [FromQuery] string? athleteName = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Llama al servicio para obtener el resumen de intentos de los atletas
                var summaries = await _athleteAttemptSummaryService.GetAthleteAttemptSummary(tournamentId, athleteId, athleteDni, athleteName, pageNumber, pageSize);
                // Retorna los resúmenes obtenidos en la respuesta HTTP con estado 200 OK
                return Ok(summaries);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener el resumen de intentos de los deportistas");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}