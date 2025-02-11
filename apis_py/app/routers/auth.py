from fastapi import APIRouter, Depends, HTTPException, status
from app.schemas.auth import AuthRequest
from app.auth.jwt_handler import create_jwt
from app.config import settings

router = APIRouter(
    prefix="/auth",
    tags=["auth"]
)

@router.post("/login")
def login(auth_request: AuthRequest):
    """
    Autentica un usuario y devuelve un token JWT para acceso autenticado.

    Parámetros:
        - **auth_request (AuthRequest)**: Objeto que contiene las credenciales de usuario.
            - **username (str)**: Nombre de usuario.
            - **password (str)**: Contraseña del usuario.

    Retorna:
        - **dict**: Contiene el token de acceso y su información.
            - **access_token (str)**: Token JWT generado para autenticación.
            - **token_type (str)**: Tipo de token (Bearer).
            - **status (str)**: Estado de la autenticación ("success" en caso de éxito).
            - **expires_at (float)**: Marca de tiempo UNIX de la expiración del token.

    Excepciones:
        - **HTTPException 401**: Si el nombre de usuario o la contraseña son incorrectos.
    """
    if auth_request.username != settings.USERNAME or auth_request.password != settings.PASSWORD:
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="Usuario/Contraseña inválido",
            headers={"WWW-Authenticate": "Bearer"},
        )

    jwt_data = create_jwt({"sub": auth_request.username})

    return {
        "access_token": jwt_data["token"],
        "token_type": "bearer",
        "status": "success",
        "expires_at": jwt_data["exp"]
    }