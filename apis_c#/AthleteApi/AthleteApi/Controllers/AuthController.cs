using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AthleteApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Constructor que inicializa la configuración
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Método HTTP POST para el inicio de sesión
        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Inicia sesión",
            Description = "Inicia sesión con las credenciales del usuario y genera un token JWT. Los campos requeridos son:\n" +
                          "- `Username`: Nombre de usuario.\n" +
                          "- `Password`: Contraseña del usuario."
        )]
        [SwaggerResponse(200, "Inicio de sesión exitoso", typeof(ApiResponse))]
        [SwaggerResponse(401, "Nombre de usuario o contraseña inválidos", typeof(ApiResponse))]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            // Aquí deberías validar las credenciales del usuario
            if (userLogin.Username == _configuration["Auth:Username"] && userLogin.Password == _configuration["Auth:Password"])
            {
                // Genera un token JWT si las credenciales son válidas
                var token = GenerateJwtToken(userLogin.Username);
                return Ok(new { token });
            }

            // Retorna una respuesta HTTP con estado 401 Unauthorized si las credenciales son inválidas
            return Unauthorized(new ApiResponse("Invalid username or password", 401));
        }

        // Método privado para generar un token JWT
        private string GenerateJwtToken(string username)
        {
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException(nameof(jwtKey), "JWT key is not configured.");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}