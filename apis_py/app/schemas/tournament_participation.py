from pydantic import BaseModel, Field

class TournamentParticipationCreate(BaseModel):
    """
    Esquema de creación para la entidad TournamentParticipation.

    Atributos:
        athlete_id (int): Identificador del atleta. Ejemplo: 1.
        tournament_id (int): Identificador del torneo. Ejemplo: 1.
        weight_category_id (int): Identificador de la categoría de peso. Ejemplo: 1.
    """
    athlete_id: int = Field(..., title="ID del atleta", description="Identificador del atleta", example=1)
    tournament_id: int = Field(..., title="ID del torneo", description="Identificador del torneo", example=1)
    weight_category_id: int = Field(..., title="ID de la categoría de peso", description="Identificador de la categoría de peso", example=1)

class TournamentParticipationResponse(BaseModel):
    """
    Esquema de respuesta para la entidad TournamentParticipation.
    
    Atributos:
        id (int): Identificador único de la participación en el torneo. Ejemplo: 1.
        athlete_id (int): Identificador del atleta. Ejemplo: 1.
        tournament_id (int): Identificador del torneo. Ejemplo: 1.
        weight_category_id (int): Identificador de la categoría de peso. Ejemplo: 1.
    """
    id: int = Field(..., title="ID de la participación", description="Identificador único de la participación en el torneo", example=1)
    athlete_id: int = Field(..., title="ID del atleta", description="Identificador del atleta", example=1)
    tournament_id: int = Field(..., title="ID del torneo", description="Identificador del torneo", example=1)
    weight_category_id: int = Field(..., title="ID de la categoría de peso", description="Identificador de la categoría de peso", example=1)

    class Config:
        from_attributes = True  # Permite usar atributos de la clase para serialización