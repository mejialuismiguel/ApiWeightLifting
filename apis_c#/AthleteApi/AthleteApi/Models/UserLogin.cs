using Swashbuckle.AspNetCore.Annotations;

namespace AthleteApi.Models
{
    [SwaggerSchema(Description = "Representa las credenciales de inicio de sesión de un usuario")]
    public class UserLogin
    {
        [SwaggerSchema("Nombre de usuario")]
        public required string Username { get; set; } = string.Empty;

        [SwaggerSchema("Contraseña del usuario")]
        public required string Password { get; set; } = string.Empty;
    }
}