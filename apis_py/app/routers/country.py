from fastapi import APIRouter, Depends, Query, status, HTTPException
from typing import List, Optional
from app.schemas.country import CountryResponse, CountryCreate
from app.services.country_service import CountryService
from app.auth.jwt_bearer import JWTBearer

router = APIRouter(
    prefix="/Country",
    tags=["Country"],
    dependencies=[Depends(JWTBearer())]
)

@router.get("/", response_model=List[CountryResponse])
def get_countries(
    page: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    size: int = Query(10, title="Tamaño de página", description="Cantidad de países por página", ge=1),
    id: Optional[str] = Query(None, title="ID del país", description="Filtrar por ID del país"),
    name: Optional[str] = Query(None, title="Nombre del país", description="Filtrar por nombre del país"),
    service: CountryService = Depends()
):
    """
    Obtiene una lista paginada de países registrados en el sistema.

    Parámetros:
        - **page (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **size (int)**: Cantidad de países a devolver por página (mínimo 1, por defecto 10).
        - **id (str, opcional)**: Filtra los países por su ID.
        - **name (str, opcional)**: Filtra los países por su nombre.

    Retorna:
        - **List[CountryResponse]**: Lista de países registrados en la página solicitada.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para acceder a la información.
    """
    return service.get_countries(page, size, id, name)

@router.post("/", response_model=CountryResponse, status_code=status.HTTP_201_CREATED)
def create_country(
    country: CountryCreate,
    service: CountryService = Depends()
):
    """
    Crea un nuevo país en el sistema.

    Parámetros:
        - **country (CountryCreate)**: Objeto que contiene los datos del país a crear.

    Retorna:
        - **CountryResponse**: Objeto que representa el país creado.

    Excepciones:
        - **HTTPException 400**: Si el país ya existe o los datos son inválidos.
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para crear un país.
    """
    try:
        return service.create_country(country)
    except ValueError as e:
        raise HTTPException(status_code=status.HTTP_400_BAD_REQUEST, detail=str(e))