from fastapi import Depends, HTTPException, status
from app.auth.jwt_bearer import JWTBearer
from app.auth.jwt_handler import decode_jwt

def get_current_user(token: str = Depends(JWTBearer())):
    """
    Proveedor de dependencias que obtiene el usuario actual autenticado a partir de un token JWT.

    Args:
        token (str): Token JWT proporcionado por el cliente.

    Returns:
        dict: Payload decodificado del token JWT que contiene la información del usuario.

    Raises:
        HTTPException: Si el token es inválido o ha expirado.
    """
    try:
        payload = decode_jwt(token)
        return payload
    except:
        raise HTTPException(
            status_code=status.HTTP_403_FORBIDDEN,
            detail="Invalid token or expired token."
        )
