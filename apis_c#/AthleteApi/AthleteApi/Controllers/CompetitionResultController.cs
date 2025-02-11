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
    public class CompetitionResultController : ControllerBase
    {
        private readonly ICompetitionResultService _competitionResultService;
        private readonly ILogger<CompetitionResultController> _logger;

        // Constructor que inicializa el servicio de resultados de competencias y el logger
        public CompetitionResultController(ICompetitionResultService competitionResultService, ILogger<CompetitionResultController> logger)
        {
            _competitionResultService = competitionResultService;
            _logger = logger;
        }

        // Método HTTP GET para obtener los resultados de la competencia con paginación y filtros opcionales
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene los resultados de la competencia",
            Description = "Obtiene los resultados de la competencia con paginación y filtros opcionales. " +
                          "Los filtros disponibles son:\n" +
                          "- `tournamentId`: Filtra por el identificador del torneo.\n" +
                          "- `tournamentName`: Filtra por el nombre del torneo.\n" +
                          "La paginación se controla con los parámetros `pageNumber` y `pageSize`."
        )]
        [SwaggerResponse(200, "Resultados de la competencia obtenidos exitosamente", typeof(IEnumerable<CompetitionResult>))]
        [SwaggerResponse(400, "Solicitud incorrecta, se requiere al menos uno de los parámetros obligatorios", typeof(ApiResponse))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> GetCompetitionResults(
            [FromQuery] int? tournamentId = null, [FromQuery] string? tournamentName = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Verifica que al menos uno de los parámetros obligatorios esté presente
            if (!tournamentId.HasValue && string.IsNullOrEmpty(tournamentName))
            {
                return BadRequest(new ApiResponse("Se tiene que proveer obligatoriamente uno de estos valores: tournamentID o tournamentName.", 400));
            }

            try
            {
                // Llama al servicio para obtener los resultados de la competencia
                var results = await _competitionResultService.GetCompetitionResults(tournamentId, tournamentName, pageNumber, pageSize);
                // Retorna los resultados obtenidos en la respuesta HTTP con estado 200 OK
                return Ok(results);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener los resultados de la competencia");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}