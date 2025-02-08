import jwt
from datetime import datetime, timedelta, timezone
from app.config import settings

def create_jwt(data: dict) -> dict:
    """
    Genera un token JWT con los datos proporcionados y una fecha de expiración.

    Args:
        data (dict): Datos que se incluirán en el payload del token.

    Returns:
        dict: Un diccionario con el token generado y su fecha de expiración.
    """
    to_encode = data.copy()  # Copia los datos para no modificarlos directamente
    expire = datetime.now(timezone.utc) + timedelta(seconds=settings.JWT_EXPIRATION_TIME)  
    # Se define la fecha de expiración con zona horaria UTC
    to_encode.update({"exp": expire.timestamp()})  # Se agrega la expiración en formato timestamp
    encoded_jwt = jwt.encode(to_encode, settings.JWT_SECRET, algorithm=settings.JWT_ALGORITHM)  
    # Se codifica el JWT con la clave secreta y el algoritmo definido
    return {"token": encoded_jwt, "exp": expire}  
    # Retorna el token generado junto con su fecha de expiración

def decode_jwt(token: str) -> dict:
    """
    Decodifica un token JWT y verifica su validez.

    Args:
        token (str): El token JWT a decodificar.

    Returns:
        dict | None: Los datos decodificados si el token es válido y no ha expirado; de lo contrario, None.
    """
    try:
        decoded_token = jwt.decode(token, settings.JWT_SECRET, algorithms=[settings.JWT_ALGORITHM])  
        # Decodifica el token usando la clave secreta y el algoritmo definido
        current_time = datetime.now(timezone.utc).timestamp()  # Obtiene el tiempo actual en formato timestamp con zona horaria UTC
        return decoded_token if decoded_token["exp"] >= current_time else None  
        # Retorna el token decodificado solo si aún no ha expirado
    except jwt.ExpiredSignatureError:
        return None  # Retorna None si la firma del token ha expirado
    except jwt.InvalidTokenError:
        return None  # Retorna None si el token es inválido