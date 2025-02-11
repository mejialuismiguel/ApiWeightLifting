from fastapi import APIRouter, Depends, Query, HTTPException, status
from typing import List, Optional
from app.schemas.competition_result import CompetitionResultResponse
from app.services.competition_result_service import CompetitionResultService
from app.auth.jwt_bearer import JWTBearer

router = APIRouter(
    prefix="/CompetitionResult",
    tags=["CompetitionResult"],
    dependencies=[Depends(JWTBearer())]
)

@router.get("/", response_model=List[CompetitionResultResponse])
def get_competition_results(
    tournament_id: Optional[int] = Query(None, title="ID del torneo", description="Identificador del torneo"),
    tournament_name: Optional[str] = Query(None, title="Nombre del torneo", description="Nombre del torneo"),
    page_number: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    page_size: int = Query(10, title="Tamaño de página", description="Cantidad de resultados por página", ge=1),
    service: CompetitionResultService = Depends()
):
    """
    Obtiene una lista paginada de resultados de competiciones.

    Parámetros:
        - **tournament_id (int, opcional)**: Identificador del torneo.
        - **tournament_name (str, opcional)**: Nombre del torneo.
        - **page_number (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **page_size (int)**: Cantidad de resultados a devolver por página (mínimo 1, por defecto 10).

    Retorna:
        - **List[CompetitionResultResponse]**: Lista de resultados de competiciones.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para acceder a la información.
    """
    return service.get_competition_results(tournament_id, tournament_name, page_number, page_size)