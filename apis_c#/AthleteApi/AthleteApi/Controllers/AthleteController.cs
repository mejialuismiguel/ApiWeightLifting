using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AthleteApi.Models;
using AthleteApi.Services;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Crea un nuevo atleta"
                            , Description = """
                                                Parámetros:
                                                    - **athlete (AthleteCreate)**: Objeto con los datos del atleta a registrar.
                                                        - **dni (str)**: Número de identificación del atleta.
                                                        - **first_name (str)**: Nombre del atleta.
                                                        - **last_name (str)**: Apellido del atleta.
                                                        - **birth_date (date)**: Fecha de nacimiento del atleta.
                                                        - **gender (str)**: Género del atleta ('M' para masculino, 'F' para femenino).
                                                        - **country_id (int)**: ID del país de origen del atleta.
                                                        - **weight_category_id (int)**: ID de la categoría de peso del atleta.

                                                Retorna:
                                                    - **AthleteResponse**: El atleta recién creado con su información registrada.

                                                Excepciones:
                                                    - **HTTPException 401**: Si el usuario no está autenticado.
                                                    - **HTTPException 400**: Si los datos del atleta son inválidos o faltan campos requeridos.
                                                """)]
        [SwaggerResponse(201, "El atleta fue creado exitosamente", typeof(Athlete))]
        [SwaggerResponse(400, "El atleta es nulo o los datos son inválidos")]
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
        [SwaggerOperation(Summary = "Obtiene una lista de atletas"
                            , Description =     """
                                                    Obtiene una lista paginada de atletas registrados en el sistema.

                                                    Parámetros:
                                                        - **page (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
                                                        - **size (int)**: Cantidad de atletas a devolver por página (mínimo 1, por defecto 10).
                                                        - **dni (str, opcional)**: Filtra atletas por su número de identificación.
                                                        - **first_name (str, opcional)**: Filtra atletas por su nombre.
                                                        - **last_name (str, opcional)**: Filtra atletas por su apellido.

                                                    Retorna:
                                                        - **List[AthleteResponse]**: Una lista de atletas en la página solicitada.

                                                    Excepciones:
                                                        - **HTTPException 401**: Si el usuario no está autenticado.
                                                    """)]
        [SwaggerResponse(200, "Lista de atletas obtenida exitosamente", typeof(IEnumerable<Athlete>))]
        [SwaggerResponse(404, "El atleta no fue encontrado")]
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