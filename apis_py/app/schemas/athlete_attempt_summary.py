from pydantic import BaseModel, Field

class AthleteAttemptSummaryResponse(BaseModel):
    """
    Esquema de respuesta para el resumen de intentos de los atletas.

    Atributos:
        athlete_name (str): Nombre del atleta. Ejemplo: "John Doe".
        athlete_dni (str): DNI del atleta. Ejemplo: "12345678".
        tournament_name (str): Nombre del torneo. Ejemplo: "Torneo Nacional".
        tournament_id (int): Identificador del torneo. Ejemplo: 1.
        total_attempts (int): Total de intentos realizados por el atleta. Ejemplo: 6.
    """
    AthleteName: str = Field(..., title="Nombre del atleta", description="Nombre del atleta", example="John Doe")
    AthleteDni: str = Field(..., title="DNI del atleta", description="DNI del atleta", example="12345678")
    TournamentName: str = Field(..., title="Nombre del torneo", description="Nombre del torneo", example="Torneo Nacional")
    TournamentId: int = Field(..., title="ID del torneo", description="Identificador del torneo", example=1)
    TotalAttempts: int = Field(..., title="Total de intentos", description="Total de intentos realizados por el atleta", example=6)