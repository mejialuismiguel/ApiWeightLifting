import os
from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    """
    Clase de configuración para la aplicación que utiliza variables de entorno.

    Atributos:
        DATABASE_URL (str): URL de conexión a la base de datos. Valor predeterminado: "mssql+pyodbc://sa:3st-eslaclave@localhost/weightlifting?driver=ODBC+Driver+17+for+SQL+Server".
        JWT_SECRET (str): Secreto utilizado para firmar tokens JWT. Valor predeterminado: "3st-eslaclave".
        JWT_ALGORITHM (str): Algoritmo utilizado para firmar tokens JWT. Valor predeterminado: "HS256".
        JWT_EXPIRATION_TIME (int): Tiempo de expiración de los tokens JWT en segundos. Valor predeterminado: 3600.
        USERNAME (str): Nombre de usuario predeterminado. Valor predeterminado: "UsuarioPrueba".
        PASSWORD (str): Contraseña predeterminada. Valor predeterminado: "yourSecurePassword123".
    """

    DATABASE_URL: str = os.getenv("DATABASE_URL", "mssql+pyodbc://sa:3st-eslaclave@localhost/weightlifting?driver=ODBC+Driver+17+for+SQL+Server")
    JWT_SECRET: str = os.getenv("JWT_SECRET", "3st-eslaclave")
    JWT_ALGORITHM: str = os.getenv("JWT_ALGORITHM", "HS256")
    JWT_EXPIRATION_TIME: int = os.getenv("JWT_EXPIRATION_TIME", 3600)
    USERNAME: str = os.getenv("USERNAME", "UsuarioPrueba")
    PASSWORD: str = os.getenv("PASSWORD", "yourSecurePassword123")

settings = Settings()
