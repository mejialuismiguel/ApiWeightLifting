from fastapi import APIRouter, Depends, Query, HTTPException, status
from typing import List, Optional
from app.schemas.athlete_attempt_summary import AthleteAttemptSummaryResponse
from app.services.athlete_attempt_summary_service import AthleteAttemptSummaryService
from app.auth.jwt_bearer import JWTBearer

router = APIRouter(
    prefix="/athlete_attempt_summary",
    tags=["athlete_attempt_summary"],
    dependencies=[Depends(JWTBearer())]
)

@router.get("/", response_model=List[AthleteAttemptSummaryResponse])
def get_athlete_attempt_summary(
    tournament_id: Optional[int] = Query(None, title="ID del torneo", description="Identificador del torneo"),
    athlete_id: Optional[int] = Query(None, title="ID del atleta", description="Identificador del atleta"),
    athlete_dni: Optional[str] = Query(None, title="DNI del atleta", description="DNI del atleta"),
    athlete_name: Optional[str] = Query(None, title="Nombre del atleta", description="Nombre del atleta"),
    page_number: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    page_size: int = Query(10, title="Tamaño de página", description="Cantidad de resúmenes por página", ge=1),
    service: AthleteAttemptSummaryService = Depends()
):
    """
    Obtiene un resumen paginado de los intentos de los atletas.

    Parámetros:
        - **tournament_id (int, opcional)**: Identificador del torneo.
        - **athlete_id (int, opcional)**: Identificador del atleta.
        - **athlete_dni (str, opcional)**: DNI del atleta.
        - **athlete_name (str, opcional)**: Nombre del atleta.
        - **page_number (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **page_size (int)**: Cantidad de resúmenes a devolver por página (mínimo 1, por defecto 10).

    Retorna:
        - **List[AthleteAttemptSummaryResponse]**: Lista de resúmenes de intentos de los atletas.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para acceder a la información.
    """
    return service.get_athlete_attempt_summary(tournament_id, athlete_id, athlete_dni, athlete_name, page_number, page_size)