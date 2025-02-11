from fastapi import APIRouter, Depends, HTTPException, Query
from typing import List, Optional
from app.schemas.tournament import TournamentCreate, TournamentResponse
from app.services.tournament_service import TournamentService
from app.auth.jwt_bearer import JWTBearer

router = APIRouter(
    prefix="/Tournament",
    tags=["Tournament"],
    dependencies=[Depends(JWTBearer())]
)

@router.get("/", response_model=List[TournamentResponse])
def get_tournaments(
    page: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    size: int = Query(10, title="Tamaño de página", description="Cantidad de torneos por página", ge=1),
    name: Optional[str] = Query(None, title="Nombre del torneo", description="Filtrar por nombre del torneo"),
    code: Optional[str] = Query(None, title="Código del torneo", description="Filtrar por código del torneo"),
    service: TournamentService = Depends()
):
    """
    Obtiene una lista paginada de torneos registrados en el sistema.

    Parámetros:
        - **page (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **size (int)**: Cantidad de torneos a devolver por página (mínimo 1, por defecto 10).
        - **name (str, opcional)**: Filtra los torneos por su nombre.
        - **code (str, opcional)**: Filtra los torneos por su código.

    Retorna:
        - **List[TournamentResponse]**: Lista de torneos en la página solicitada.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para acceder a la información.
    """
    return service.get_tournaments(page, size, name, code)

@router.post("/", response_model=TournamentResponse)
def create_tournament(
    tournament: TournamentCreate, 
    service: TournamentService = Depends()
):
    """
    Crea un nuevo torneo en el sistema.

    Parámetros:
        - **tournament (TournamentCreate)**: Objeto con los datos del torneo.
            - **name (str)**: Nombre del torneo.
            - **code (str)**: Código único del torneo.
            - **start_date (datetime)**: Fecha de inicio del torneo.
            - **end_date (datetime)**: Fecha de finalización del torneo.

    Retorna:
        - **TournamentResponse**: Información del torneo creado.

    Excepciones:
        - **HTTPException 400**: Si hay datos inválidos o faltantes.
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para crear torneos.
    """
    return service.create_tournament(tournament)