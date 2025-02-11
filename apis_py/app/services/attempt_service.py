from typing import List, Optional
from fastapi import Depends
from sqlalchemy.orm import Session
from sqlalchemy import text
from app.schemas.attempt import AttemptCreate, AttemptResponse
from app.models.attempt import Attempt
from app.database import get_db

class AttemptService:
    """
    Servicio para manejar operaciones relacionadas con intentos en la base de datos.
    
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

    def get_attempts_by_tournament(self, tournament_id: Optional[int], tournament_name: Optional[str], page_number: int, page_size: int) -> List[AttemptResponse]:
        """
        Obtiene una lista paginada de intentos por torneo desde la base de datos.

        Args:
            tournament_id (Optional[int]): Identificador del torneo.
            tournament_name (Optional[str]): Nombre del torneo.
            page_number (int): Número de la página a obtener.
            page_size (int): Cantidad de registros por página.

        Returns:
            List[AttemptResponse]: Lista de respuestas con la información de los intentos.
        """
        attempts = self.db.execute(
            text("EXEC sp_getattemptsbytournament @TournamentId = :tournament_id, @TournamentName = :tournament_name, @PageNumber = :page_number, @PageSize = :page_size"),
            {'tournament_id': tournament_id, 'tournament_name': tournament_name, 'page_number': page_number, 'page_size': page_size}
        ).fetchall()
        return [AttemptResponse.model_validate(attempt) for attempt in attempts]

    def add_attempt(self, attempt: AttemptCreate) -> AttemptResponse:
        """
        Agrega un nuevo intento en la base de datos.

        Args:
            attempt (AttemptCreate): Objeto que contiene los datos del intento a crear.

        Returns:
            AttemptResponse: Objeto que representa el intento creado.

        Raises:
            ValueError: Si el intento no cumple con las reglas de negocio.
        """
        self.db.execute(
            text("EXEC sp_addattempt @ParticipationId = :participation_id, @AttemptNumber = :attempt_number, @Type = :type, @WeightLifted = :weight_lifted, @Success = :success"),
            {'participation_id': attempt.participation_id, 'attempt_number': attempt.attempt_number, 'type': attempt.type, 'weight_lifted': attempt.weight_lifted, 'success': attempt.success}
        )
        self.db.commit()
        new_attempt = self.db.query(Attempt).filter_by(participation_id=attempt.participation_id, attempt_number=attempt.attempt_number, type=attempt.type).first()
        return AttemptResponse.model_validate(new_attempt)