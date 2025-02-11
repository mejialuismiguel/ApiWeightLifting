from fastapi import APIRouter, Depends, Query, HTTPException, status
from typing import List, Optional
from app.schemas.tournament_participation import TournamentParticipationResponse, TournamentParticipationCreate
from app.services.tournament_participation_service import TournamentParticipationService
from app.auth.jwt_bearer import JWTBearer

router = APIRouter(
    prefix="/tournament_participation",
    tags=["tournament_participation"],
    dependencies=[Depends(JWTBearer())]
)

@router.get("/", response_model=List[TournamentParticipationResponse])
def get_participants(
    tournament_id: Optional[int] = Query(None, title="ID del torneo", description="Identificador del torneo"),
    athlete_name: Optional[str] = Query(None, title="Nombre del atleta", description="Nombre del atleta"),
    page_number: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    page_size: int = Query(10, title="Tamaño de página", description="Cantidad de participaciones por página", ge=1),
    service: TournamentParticipationService = Depends()
):
    """
    Obtiene una lista paginada de participantes en un torneo.

    Parámetros:
        - **tournament_id (int, opcional)**: Identificador del torneo.
        - **athlete_name (str, opcional)**: Nombre del atleta.
        - **page_number (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **page_size (int)**: Cantidad de participaciones a devolver por página (mínimo 1, por defecto 10).

    Retorna:
        - **List[TournamentParticipationResponse]**: Lista de participantes en el torneo.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para acceder a la información.
    """
    return service.get_participants(tournament_id, athlete_name, page_number, page_size)

@router.post("/", response_model=TournamentParticipationResponse, status_code=status.HTTP_201_CREATED)
def add_participant(
    participation: TournamentParticipationCreate,
    service: TournamentParticipationService = Depends()
):
    """
    Agrega un nuevo participante en un torneo.

    Parámetros:
        - **participation (TournamentParticipationCreate)**: Objeto que contiene los datos de la participación a crear.

    Retorna:
        - **TournamentParticipationResponse**: Objeto que representa la participación creada.

    Excepciones:
        - **HTTPException 400**: Si la participación ya existe o los datos son inválidos.
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para agregar una participación.
    """
    try:
        return service.add_participant(participation)
    except ValueError as e:
        raise HTTPException(status_code=status.HTTP_400_BAD_REQUEST, detail=str(e))