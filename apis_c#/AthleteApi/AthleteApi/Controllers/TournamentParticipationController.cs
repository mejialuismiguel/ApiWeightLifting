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
    public class TournamentParticipationController : ControllerBase
    {
        private readonly ITournamentParticipationService _tournamentParticipationService;
        private readonly ILogger<TournamentParticipationController> _logger;

        // Constructor que inicializa el servicio de participación en torneos y el logger
        public TournamentParticipationController(ITournamentParticipationService tournamentParticipationService, ILogger<TournamentParticipationController> logger)
        {
            _tournamentParticipationService = tournamentParticipationService;
            _logger = logger;
        }

        // Método HTTP POST para agregar un nuevo participante en un torneo
        [HttpPost]
        [SwaggerOperation(
            Summary = "Agrega un nuevo participante en un torneo",
            Description = "Agrega un nuevo participante en un torneo de levantamiento de pesas. Los campos requeridos son:\n" +
                          "- `AthleteId`: Identificador del atleta.\n" +
                          "- `TournamentId`: Identificador del torneo.\n" +
                          "- `WeightCategoryId`: Identificador de la categoría de peso."
        )]
        [SwaggerResponse(200, "Participante agregado satisfactoriamente", typeof(ApiResponse))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> AddParticipant([FromBody] TournamentParticipation participation)
        {
            try
            {
                // Llama al servicio para agregar un nuevo participante
                await _tournamentParticipationService.AddParticipant(participation);
                // Retorna una respuesta HTTP con estado 200 OK y un mensaje de éxito
                return Ok(new ApiResponse("Participante agregado satisfactoriamente", 0));
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al agregar el participante");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }

        // Método HTTP GET para obtener una lista de participaciones en torneos con paginación y filtro opcional por torneo y nombre del atleta
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene una lista de participaciones en torneos",
            Description = "Obtiene una lista de participaciones en torneos con paginación y filtro opcional por torneo y nombre del atleta. " +
                          "Los filtros disponibles son:\n" +
                          "- `tournamentId`: Filtra por el identificador del torneo.\n" +
                          "- `athleteName`: Filtra por el nombre del atleta.\n" +
                          "La paginación se controla con los parámetros `pageNumber` y `pageSize`."
        )]
        [SwaggerResponse(200, "Lista de participaciones obtenida exitosamente", typeof(IEnumerable<TournamentParticipation>))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> GetTournamentParticipations(
            [FromQuery] int? tournamentId = null, [FromQuery] string? athleteName = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Llama al servicio para obtener la lista de participaciones en torneos
                var participations = await _tournamentParticipationService.GetParticipants(tournamentId, athleteName, pageNumber, pageSize);
                // Retorna la lista de participaciones obtenida en la respuesta HTTP con estado 200 OK
                return Ok(participations);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener las participaciones en el torneo");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}