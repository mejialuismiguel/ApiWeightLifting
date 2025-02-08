from fastapi import Request, HTTPException
from fastapi.security import HTTPBearer, HTTPAuthorizationCredentials
from app.auth.jwt_handler import decode_jwt

class JWTBearer(HTTPBearer):
    """
    Clase de autenticación basada en JWT para FastAPI.

    Esta clase extiende `HTTPBearer` y se encarga de validar tokens JWT en 
    las solicitudes entrantes. Verifica que el esquema de autenticación sea "Bearer"
    y que el token sea válido y no haya expirado.

    Attributes:
        auto_error (bool): Determina si se debe lanzar automáticamente una excepción en caso de error.
    """

    def __init__(self, auto_error: bool = True):
        """
        Inicializa la autenticación JWT con opción de manejar errores automáticamente.

        Args:
            auto_error (bool): Si es `True`, lanza una excepción en caso de error de autenticación.
        """
        super(JWTBearer, self).__init__(auto_error=auto_error)

    async def __call__(self, request: Request):
        """
        Método que intercepta las solicitudes y valida el token JWT.

        Args:
            request (Request): La solicitud HTTP entrante.

        Returns:
            str: El token JWT válido si la autenticación es exitosa.

        Raises:
            HTTPException: Si el esquema de autenticación no es "Bearer", el token es inválido o ha expirado.
        """
        credentials: HTTPAuthorizationCredentials = await super(JWTBearer, self).__call__(request)
        if credentials:
            if not credentials.scheme == "Bearer":
                raise HTTPException(status_code=403, detail="Invalid authentication scheme.")  
                # Se lanza un error si el esquema no es "Bearer"
            if not self.verify_jwt(credentials.credentials):
                raise HTTPException(status_code=403, detail="Invalid token or expired token.")  
                # Se lanza un error si el token es inválido o ha expirado
            return credentials.credentials  # Retorna el token si es válido
        else:
            raise HTTPException(status_code=403, detail="Invalid authorization code.")  
            # Se lanza un error si no se proporcionó un código de autorización válido

    def verify_jwt(self, jwtoken: str) -> bool:
        """
        Verifica si un token JWT es válido y no ha expirado.

        Args:
            jwtoken (str): Token JWT a validar.

        Returns:
            bool: `True` si el token es válido, `False` en caso contrario.
        """
        is_token_valid: bool = False
        payload = decode_jwt(jwtoken)  # Intenta decodificar el token
        if payload:
            is_token_valid = True  # Si hay un payload válido, el token es correcto
        return is_token_valid