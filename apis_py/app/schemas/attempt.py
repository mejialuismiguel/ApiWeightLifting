from pydantic import BaseModel, Field

class AttemptCreate(BaseModel):
    """
    Esquema de creación para la entidad Attempt.

    Atributos:
        participation_id (int): Identificador de la participación. Ejemplo: 1.
        attempt_number (int): Número del intento. Ejemplo: 1.
        type (str): Tipo de intento (Arranque o Envion). Ejemplo: "Arranque".
        weight_lifted (float): Peso levantado en el intento. Ejemplo: 100.0.
        success (bool): Indica si el intento fue exitoso. Ejemplo: True.
    """
    participation_id: int = Field(..., title="ID de la participación", description="Identificador de la participación", example=1)
    attempt_number: int = Field(..., title="Número del intento", description="Número del intento", example=1)
    type: str = Field(..., title="Tipo de intento", description="Tipo de intento (Arranque o Envion)", example="Arranque")
    weight_lifted: float = Field(..., title="Peso levantado", description="Peso levantado en el intento", example=100.0)
    success: bool = Field(..., title="Éxito", description="Indica si el intento fue exitoso", example=True)

class AttemptResponse(BaseModel):
    """
    Esquema de respuesta para la entidad Attempt.
    
    Atributos:
        id (int): Identificador único del intento. Ejemplo: 1.
        participation_id (int): Identificador de la participación. Ejemplo: 1.
        attempt_number (int): Número del intento. Ejemplo: 1.
        type (str): Tipo de intento (Arranque o Envion). Ejemplo: "Arranque".
        weight_lifted (float): Peso levantado en el intento. Ejemplo: 100.0.
        success (bool): Indica si el intento fue exitoso. Ejemplo: True.
        tournament_name (str): Nombre del torneo. Ejemplo: "Torneo Nacional".
        tournament_id (int): Identificador del torneo. Ejemplo: 1.
    """
    id: int = Field(..., title="ID del intento", description="Identificador único del intento", example=1)
    participation_id: int = Field(..., title="ID de la participación", description="Identificador de la participación", example=1)
    attempt_number: int = Field(..., title="Número del intento", description="Número del intento", example=1)
    type: str = Field(..., title="Tipo de intento", description="Tipo de intento (Arranque o Envion)", example="Arranque")
    weight_lifted: float = Field(..., title="Peso levantado", description="Peso levantado en el intento", example=100.0)
    success: bool = Field(..., title="Éxito", description="Indica si el intento fue exitoso", example=True)
    tournament_name: str = Field(..., title="Nombre del torneo", description="Nombre del torneo", example="Torneo Nacional")
    tournament_id: int = Field(..., title="ID del torneo", description="Identificador del torneo", example=1)

    class Config:
        from_attributes = True  # Permite usar atributos de la clase para serialización