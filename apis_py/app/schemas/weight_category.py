from pydantic import BaseModel, Field

class WeightCategoryCreate(BaseModel):
    """
    Esquema de creación para la entidad WeightCategory.

    Atributos:
        name (str): Nombre de la categoría de peso. Ejemplo: "Lightweight".
        min_weight (float): Peso mínimo de la categoría. Ejemplo: 60.0.
        max_weight (float): Peso máximo de la categoría. Ejemplo: 70.0.
        gender (str): Género asociado a la categoría de peso. Ejemplo: "M".
    """
    name: str = Field(..., title="Nombre de la categoría de peso", description="Nombre de la categoría de peso", example="Lightweight")
    min_weight: float = Field(..., title="Peso mínimo", description="Peso mínimo de la categoría", example=60.0)
    max_weight: float = Field(..., title="Peso máximo", description="Peso máximo de la categoría", example=70.0)
    gender: str = Field(..., title="Género", description="Género asociado a la categoría de peso", example="M")

class WeightCategoryResponse(BaseModel):
    """
    Esquema de respuesta para la entidad WeightCategory.
    
    Atributos:
        id (int): Identificador único de la categoría de peso en la base de datos. Ejemplo: 1.
        name (str): Nombre de la categoría de peso. Ejemplo: "Lightweight".
        min_weight (float): Peso mínimo de la categoría. Ejemplo: 60.0.
        max_weight (float): Peso máximo de la categoría. Ejemplo: 70.0.
        gender (str): Género asociado a la categoría de peso. Ejemplo: "M".
    """
    id: int = Field(..., title="ID de la categoría de peso", description="Identificador único de la categoría de peso en la base de datos", example=1)
    name: str = Field(..., title="Nombre de la categoría de peso", description="Nombre de la categoría de peso", example="Lightweight")
    min_weight: float = Field(..., title="Peso mínimo", description="Peso mínimo de la categoría", example=60.0)
    max_weight: float = Field(..., title="Peso máximo", description="Peso máximo de la categoría", example=70.0)
    gender: str = Field(..., title="Género", description="Género asociado a la categoría de peso", example="M")

    class Config:
        from_attributes = True  # Permite usar atributos de la clase para serialización