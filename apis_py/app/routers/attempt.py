from fastapi import APIRouter, Depends, Query, HTTPException, status
from typing import List, Optional
from app.schemas.attempt import AttemptResponse, AttemptCreate
from app.services.attempt_service import AttemptService
from app.auth.jwt_bearer import JWTBearer

router = APIRouter(
    prefix="/Attempt",
    tags=["Attempt"],
    dependencies=[Depends(JWTBearer())]
)

@router.get("/", response_model=List[AttemptResponse])
def get_attempts_by_tournament(
    tournament_id: Optional[int] = Query(None, title="ID del torneo", description="Identificador del torneo"),
    tournament_name: Optional[str] = Query(None, title="Nombre del torneo", description="Nombre del torneo"),
    page_number: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    page_size: int = Query(10, title="Tamaño de página", description="Cantidad de intentos por página", ge=1),
    service: AttemptService = Depends()
):
    """
    Obtiene una lista paginada de intentos por torneo.

    Parámetros:
        - **tournament_id (int, opcional)**: Identificador del torneo.
        - **tournament_name (str, opcional)**: Nombre del torneo.
        - **page_number (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **page_size (int)**: Cantidad de intentos a devolver por página (mínimo 1, por defecto 10).

    Retorna:
        - **List[AttemptResponse]**: Lista de intentos por torneo.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para acceder a la información.
    """
    return service.get_attempts_by_tournament(tournament_id, tournament_name, page_number, page_size)

@router.post("/", response_model=AttemptResponse, status_code=status.HTTP_201_CREATED)
def add_attempt(
    attempt: AttemptCreate,
    service: AttemptService = Depends()
):
    """
    Agrega un nuevo intento en un torneo.

    Parámetros:
        - **attempt (AttemptCreate)**: Objeto que contiene los datos del intento a crear.

    Retorna:
        - **AttemptResponse**: Objeto que representa el intento creado.

    Excepciones:
        - **HTTPException 400**: Si el intento no cumple con las reglas de negocio o los datos son inválidos.
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para agregar un intento.
    """
    try:
        return service.add_attempt(attempt)
    except ValueError as e:
        raise HTTPException(status_code=status.HTTP_400_BAD_REQUEST, detail=str(e))