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
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly ILogger<TournamentController> _logger;

        // Constructor que inicializa el servicio de torneos y el logger
        public TournamentController(ITournamentService tournamentService, ILogger<TournamentController> logger)
        {
            _tournamentService = tournamentService;
            _logger = logger;
        }

        // Método HTTP POST para crear un nuevo torneo
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo torneo",
            Description = "Crea un nuevo torneo de levantamiento de pesas. Los campos requeridos son:\n" +
                          "- `Name`: Nombre del torneo.\n" +
                          "- `Location`: Ubicación del torneo.\n" +
                          "- `StartDate`: Fecha de inicio del torneo.\n" +
                          "- `EndDate`: Fecha de finalización del torneo."
        )]
        [SwaggerResponse(200, "Torneo creado satisfactoriamente", typeof(ApiResponse))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> CreateTournament([FromBody] Tournament tournament)
        {
            try
            {
                // Llama al servicio para crear un nuevo torneo
                await _tournamentService.CreateTournament(tournament);
                // Retorna una respuesta HTTP con estado 200 OK y un mensaje de éxito
                return Ok(new ApiResponse("Torneo creado satisfactoriamente", 0));
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al crear el torneo");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }

        // Método HTTP GET para obtener una lista de torneos con paginación y filtro opcional por nombre
        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene una lista de torneos",
            Description = "Obtiene una lista de torneos con paginación y filtro opcional por nombre. " +
                          "Los filtros disponibles son:\n" +
                          "- `name`: Filtra por el nombre del torneo.\n" +
                          "La paginación se controla con los parámetros `pageNumber` y `pageSize`."
        )]
        [SwaggerResponse(200, "Lista de torneos obtenida exitosamente", typeof(IEnumerable<Tournament>))]
        [SwaggerResponse(500, "Error interno del servidor", typeof(ApiResponse))]
        public async Task<IActionResult> GetTournaments(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? name = null)
        {
            try
            {
                // Llama al servicio para obtener la lista de torneos
                var tournaments = await _tournamentService.GetTournaments(pageNumber, pageSize, name);
                // Retorna la lista de torneos obtenida en la respuesta HTTP con estado 200 OK
                return Ok(tournaments);
            }
            catch (Exception ex)
            {
                // Registra el error en el logger
                _logger.LogError(ex, "Error al obtener los torneos");
                // Retorna una respuesta HTTP con estado 500 Internal Server Error y un mensaje de error
                return StatusCode(500, new ApiResponse($"Internal server error: {ex.Message}", 500));
            }
        }
    }
}