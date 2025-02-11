from pydantic import BaseModel, Field
from typing import Optional

class CountryCreate(BaseModel):
    """
    Esquema de creación para la entidad Country.

    Atributos:
        name (str): Nombre oficial del país. Ejemplo: "Ecuador".
        code (str): Código ISO de tres letras que representa al país. Ejemplo: "ECU".
    """
    name: str = Field(..., title="Nombre del país", description="Nombre oficial del país", example="Ecuador")
    code: str = Field(..., title="Código ISO", description="Código de tres letras que representa al país", example="ECU")

class CountryResponse(BaseModel):
    """
    Esquema de respuesta para la entidad Country.
    
    Atributos:
        id (int): Identificador único del país en la base de datos. Ejemplo: 1.
        name (str): Nombre oficial del país. Ejemplo: "Ecuador".
        code (str): Código ISO de tres letras que representa al país. Ejemplo: "ECU".
    """

    id: int = Field(..., title="ID del país", description="Identificador único del país en la base de datos", example=1)
    name: str = Field(..., title="Nombre del país", description="Nombre oficial del país", example="Ecuador")
    code: str = Field(..., title="Código ISO", description="Código de tres letras que representa al país", example="ECU")

    class Config:
        from_attributes = True  # Permite usar atributos de la clase para serialización
