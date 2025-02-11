from typing import List, Optional
from fastapi import Depends
from sqlalchemy.orm import Session
from sqlalchemy import text
from app.schemas.athlete_attempt_summary import AthleteAttemptSummaryResponse
from app.database import get_db

class AthleteAttemptSummaryService:
    """
    Servicio para manejar operaciones relacionadas con el resumen de intentos de los atletas en la base de datos.
    
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

    def get_athlete_attempt_summary(self, tournament_id: Optional[int], athlete_id: Optional[int], athlete_dni: Optional[str], athlete_name: Optional[str], page_number: int, page_size: int) -> List[AthleteAttemptSummaryResponse]:
        """
        Obtiene un resumen paginado de los intentos de los atletas desde la base de datos.

        Args:
            tournament_id (Optional[int]): Identificador del torneo.
            athlete_id (Optional[int]): Identificador del atleta.
            athlete_dni (Optional[str]): DNI del atleta.
            athlete_name (Optional[str]): Nombre del atleta.
            page_number (int): Número de la página a obtener.
            page_size (int): Cantidad de registros por página.

        Returns:
            List[AthleteAttemptSummaryResponse]: Lista de respuestas con el resumen de los intentos de los atletas.
        """
        summaries = self.db.execute(
            text("EXEC sp_getathleteattemptsummary @TournamentId = :tournament_id, @AthleteId = :athlete_id, @AthleteDni = :athlete_dni, @AthleteName = :athlete_name, @PageNumber = :page_number, @PageSize = :page_size"),
            {'tournament_id': tournament_id, 'athlete_id': athlete_id, 'athlete_dni': athlete_dni, 'athlete_name': athlete_name, 'page_number': page_number, 'page_size': page_size}
        ).fetchall()
        
        result = []
        for summary in summaries:
            result.append(AthleteAttemptSummaryResponse(
                AthleteName=summary.AthleteName,
                AthleteDni=summary.AthleteDni,
                TournamentName=summary.TournamentName,
                TournamentId=summary.TournamentId,
                TotalAttempts=summary.TotalAttempts
            ))
        
        return result