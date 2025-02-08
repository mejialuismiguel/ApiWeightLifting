from pydantic import BaseModel, Field
from datetime import date

class TournamentCreate(BaseModel):
    """
    Esquema para la creación de un torneo.
    
    Atributos:
        name (str): Nombre oficial del torneo.
                    Longitud mínima de 3 y máxima de 50 caracteres. Ejemplo: "Beijing 2022".
        location (str): Ciudad o país donde se lleva a cabo el torneo.
                        Longitud mínima de 3 y máxima de 100 caracteres. Ejemplo: "Beijing, China".
        start_date (date): Fecha en la que comienza el torneo en formato YYYY-MM-DD. Ejemplo: "2022-08-01".
        end_date (date): Fecha en la que finaliza el torneo en formato YYYY-MM-DD. Ejemplo: "2022-08-10".
    """

    name: str = Field(..., title="Nombre del torneo", description="Nombre oficial del torneo", min_length=3, max_length=50, example="Beijing 2022")
    location: str = Field(..., title="Ubicación", description="Ciudad o país donde se lleva a cabo el torneo", min_length=3, max_length=100, example="Beijing, China")
    start_date: date = Field(..., title="Fecha de inicio", description="Fecha en la que comienza el torneo (YYYY-MM-DD)", example="2022-08-01")
    end_date: date = Field(..., title="Fecha de finalización", description="Fecha en la que finaliza el torneo (YYYY-MM-DD)", example="2022-08-10")

class TournamentResponse(BaseModel):
    """
    Esquema de respuesta para la información de un torneo.
    
    Atributos:
        id (int): Identificador único del torneo en la base de datos. Ejemplo: 1.
        name (str): Nombre oficial del torneo. Ejemplo: "Beijing 2022".
        location (str): Ciudad o país donde se lleva a cabo el torneo. Ejemplo: "Beijing, China".
        start_date (date): Fecha en la que comienza el torneo en formato YYYY-MM-DD. Ejemplo: "2022-08-01".
        end_date (date): Fecha en la que finaliza el torneo en formato YYYY-MM-DD. Ejemplo: "2022-08-10".
    """

    id: int = Field(..., title="ID del torneo", description="Identificador único del torneo en la base de datos", example=1)
    name: str = Field(..., title="Nombre del torneo", description="Nombre oficial del torneo", example="Beijing 2022")
    location: str = Field(..., title="Ubicación", description="Ciudad o país donde se lleva a cabo el torneo", example="Beijing, China")
    start_date: date = Field(..., title="Fecha de inicio", description="Fecha en la que comienza el torneo (YYYY-MM-DD)", example="2022-08-01")
    end_date: date = Field(..., title="Fecha de finalización", description="Fecha en la que finaliza el torneo (YYYY-MM-DD)", example="2022-08-10")

    class Config:
        from_attributes = True  # Compatibilidad con modelos de base de datos
