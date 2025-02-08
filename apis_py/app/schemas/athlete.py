from pydantic import BaseModel, Field
from datetime import date

class AthleteCreate(BaseModel):
    """
    Esquema para la creación de un atleta.
    
    Atributos:
        dni (str): Documento de identidad único del atleta. 
                   Longitud mínima de 5 y máxima de 20 caracteres. Ejemplo: "12345678".
        first_name (str): Nombre del atleta.
                          Longitud mínima de 2 y máxima de 50 caracteres. Ejemplo: "Juan".
        last_name (str): Apellido del atleta.
                         Longitud mínima de 2 y máxima de 50 caracteres. Ejemplo: "Pérez".
        birth_date (date): Fecha de nacimiento del atleta en formato YYYY-MM-DD. Ejemplo: "1990-01-01".
        gender (str): Género del atleta ('M' para masculino, 'F' para femenino). 
                      Debe coincidir con el patrón ^(M|F)$. Ejemplo: "M".
        country_id (int): Identificador del país al que pertenece el atleta. Ejemplo: 1.
        weight_category_id (int): Identificador de la categoría de peso en la que compite el atleta. Ejemplo: 2.
    """

    dni: str = Field(..., title="DNI", description="Documento de identidad único del atleta", min_length=5, max_length=20, example="12345678")
    first_name: str = Field(..., title="Nombre", description="Nombre del atleta", min_length=2, max_length=50, example="Juan")
    last_name: str = Field(..., title="Apellido", description="Apellido del atleta", min_length=2, max_length=50, example="Pérez")
    birth_date: date = Field(..., title="Fecha de nacimiento", description="Fecha de nacimiento del atleta (YYYY-MM-DD)", example="1990-01-01")
    gender: str = Field(..., title="Género", description="Género del atleta ('M' para masculino, 'F' para femenino)", pattern="^(M|F)$", example="M")
    country_id: int = Field(..., title="ID del país", description="Identificador del país al que pertenece el atleta", example=1)
    weight_category_id: int = Field(..., title="ID de categoría de peso", description="Identificador de la categoría de peso en la que compite el atleta", example=2)

class AthleteResponse(BaseModel):
    """
    Esquema de respuesta para la información de un atleta.
    
    Atributos:
        id (int): Identificador único del atleta en la base de datos. Ejemplo: 100.
        dni (str): Documento de identidad único del atleta. Ejemplo: "12345678".
        first_name (str): Nombre del atleta. Ejemplo: "Juan".
        last_name (str): Apellido del atleta. Ejemplo: "Pérez".
        birth_date (date): Fecha de nacimiento del atleta en formato YYYY-MM-DD. Ejemplo: "1990-01-01".
        gender (str): Género del atleta ('M' para masculino, 'F' para femenino). 
                      Debe coincidir con el patrón ^(M|F)$. Ejemplo: "M".
        country_id (int): Identificador del país al que pertenece el atleta. Ejemplo: 1.
        weight_category_id (int): Identificador de la categoría de peso en la que compite el atleta. Ejemplo: 2.
    """

    id: int = Field(..., title="ID del atleta", description="Identificador único del atleta en la base de datos", example=100)
    dni: str = Field(..., title="DNI", description="Documento de identidad único del atleta", example="12345678")
    first_name: str = Field(..., title="Nombre", description="Nombre del atleta", example="Juan")
    last_name: str = Field(..., title="Apellido", description="Apellido del atleta", example="Pérez")
    birth_date: date = Field(..., title="Fecha de nacimiento", description="Fecha de nacimiento del atleta (YYYY-MM-DD)", example="1990-01-01")
    gender: str = Field(..., title="Género", description="Género del atleta ('M' para masculino, 'F' para femenino)", pattern="^(M|F)$", example="M")
    country_id: int = Field(..., title="ID del país", description="Identificador del país al que pertenece el atleta", example=1)
    weight_category_id: int = Field(..., title="ID de categoría de peso", description="Identificador de la categoría de peso en la que compite el atleta", example=2)

    class Config:
        from_attributes = True  # Compatibilidad con modelos de base de datos
