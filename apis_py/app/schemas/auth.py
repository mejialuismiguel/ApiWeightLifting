from pydantic import BaseModel, Field

class AuthRequest(BaseModel):
    """
    Esquema para la autenticación de usuarios.
    
    Atributos:
        username (str): Nombre de usuario para autenticación. 
                        Longitud mínima de 4 y máxima de 50 caracteres. Ejemplo: "UsuarioPrueba".
        password (str): Contraseña del usuario.
                        Longitud mínima de 6 y máxima de 100 caracteres. Ejemplo: "yourSecurePassword123".
    """

    username: str = Field(..., title="Nombre de usuario", description="Nombre de usuario para autenticación", min_length=4, max_length=50, example="UsuarioPrueba")
    password: str = Field(..., title="Contraseña", description="Contraseña del usuario", min_length=6, max_length=100, example="yourSecurePassword123")
