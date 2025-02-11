from typing import List, Optional
from fastapi import Depends
from sqlalchemy.orm import Session
from sqlalchemy import text
from app.schemas.competition_result import CompetitionResultResponse
from app.database import get_db

class CompetitionResultService:
    """
    Servicio para manejar operaciones relacionadas con los resultados de las competiciones en la base de datos.
    
    Atributos:
        db (Session): Sesión de la base de datos para realizar las operaciones.
    """

    def __init__(self, db: Session = Depends(get_db)):
        """
        Inicializa el servicio con una sesión de la base de datos.
        
        Args:
            db (Session): Sesión de la base de datos inyectada automáticamente por FastAPI.
        """
        self.db = db

    def get_competition_results(self, tournament_id: Optional[int], tournament_name: Optional[str], page_number: int, page_size: int) -> List[CompetitionResultResponse]:
        """
        Obtiene una lista paginada de resultados de competiciones desde la base de datos.

        Args:
            tournament_id (Optional[int]): Identificador del torneo.
            tournament_name (Optional[str]): Nombre del torneo.
            page_number (int): Número de la página a obtener.
            page_size (int): Cantidad de registros por página.

        Returns:
            List[CompetitionResultResponse]: Lista de respuestas con los resultados de las competiciones.
        """
        results = self.db.execute(
            text("EXEC sp_getcompetitionresults @TournamentId = :tournament_id, @TournamentName = :tournament_name, @PageNumber = :page_number, @PageSize = :page_size"),
            {'tournament_id': tournament_id, 'tournament_name': tournament_name, 'page_number': page_number, 'page_size': page_size}
        ).fetchall()
        
        return [CompetitionResultResponse(
            CountryCode=result.CountryCode,
            AthleteName=result.AthleteName,
            MaxSnatch=result.MaxSnatch,
            MaxCleanAndJerk=result.MaxCleanAndJerk,
            TotalWeight=result.TotalWeight
        ) for result in results]