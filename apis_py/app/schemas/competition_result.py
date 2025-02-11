from pydantic import BaseModel, Field

class CompetitionResultResponse(BaseModel):
    """
    Esquema de respuesta para los resultados de las competiciones.

    Atributos:
        country_code (str): Código del país del atleta. Ejemplo: "USA".
        athlete_name (str): Nombre del atleta. Ejemplo: "John Doe".
        max_snatch (float): Máximo peso levantado en arranque. Ejemplo: 100.0.
        max_clean_and_jerk (float): Máximo peso levantado en envión. Ejemplo: 120.0.
        total_weight (float): Peso total levantado. Ejemplo: 220.0.
    """
    CountryCode: str = Field(..., title="Código del país", description="Código del país del atleta", example="USA")
    AthleteName: str = Field(..., title="Nombre del atleta", description="Nombre del atleta", example="John Doe")
    MaxSnatch: float = Field(..., title="Máximo arranque", description="Máximo peso levantado en arranque", example=100.0)
    MaxCleanAndJerk: float = Field(..., title="Máximo envión", description="Máximo peso levantado en envión", example=120.0)
    TotalWeight: float = Field(..., title="Peso total", description="Peso total levantado", example=220.0)