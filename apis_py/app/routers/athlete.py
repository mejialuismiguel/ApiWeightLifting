from fastapi import APIRouter, Depends, HTTPException, Query
from typing import List, Optional
from app.schemas.athlete import AthleteCreate, AthleteResponse
from app.services.athlete_service import AthleteService
from app.dependencies import get_current_user

router = APIRouter(
    prefix="/athlete",
    tags=["athlete"],
    dependencies=[Depends(get_current_user)]
)

@router.get("/", response_model=List[AthleteResponse])
def get_athletes(
    page: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    size: int = Query(10, title="Tamaño de página", description="Cantidad de atletas por página", ge=1),
    dni: Optional[str] = Query(None, title="DNI del atleta", description="Filtrar por número de identificación"),
    first_name: Optional[str] = Query(None, title="Nombre del atleta", description="Filtrar por nombre del atleta"),
    last_name: Optional[str] = Query(None, title="Apellido del atleta", description="Filtrar por apellido del atleta"),
    service: AthleteService = Depends()
):
    """
    Obtiene una lista paginada de atletas registrados en el sistema.

    Parámetros:
        - **page (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **size (int)**: Cantidad de atletas a devolver por página (mínimo 1, por defecto 10).
        - **dni (str, opcional)**: Filtra atletas por su número de identificación.
        - **first_name (str, opcional)**: Filtra atletas por su nombre.
        - **last_name (str, opcional)**: Filtra atletas por su apellido.

    Retorna:
        - **List[AthleteResponse]**: Una lista de atletas en la página solicitada.
    
    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
    """
    return service.get_athletes(page, size, dni, first_name, last_name)

@router.post("/", response_model=AthleteResponse)
def create_athlete(
    athlete: AthleteCreate, 
    service: AthleteService = Depends()
):
    """
    Crea un nuevo atleta en el sistema.

    Parámetros:
        - **athlete (AthleteCreate)**: Objeto con los datos del atleta a registrar.
            - **dni (str)**: Número de identificación del atleta.
            - **first_name (str)**: Nombre del atleta.
            - **last_name (str)**: Apellido del atleta.
            - **birth_date (date)**: Fecha de nacimiento del atleta.
            - **gender (str)**: Género del atleta ('M' para masculino, 'F' para femenino).
            - **country_id (int)**: ID del país de origen del atleta.
            - **weight_category_id (int)**: ID de la categoría de peso del atleta.

    Retorna:
        - **AthleteResponse**: El atleta recién creado con su información registrada.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 400**: Si los datos del atleta son inválidos o faltan campos requeridos.
    """
    return service.create_athlete(athlete)