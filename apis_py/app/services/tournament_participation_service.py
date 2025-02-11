from typing import List, Optional
from fastapi import Depends
from sqlalchemy.orm import Session
from sqlalchemy import text
from app.schemas.tournament_participation import TournamentParticipationCreate, TournamentParticipationResponse
from app.database import get_db
from app.models.tournament_participation import TournamentParticipation

class TournamentParticipationService:
    """
    Servicio para manejar operaciones relacionadas con participaciones en torneos en la base de datos.
    
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

    def get_participants(self, tournament_id: Optional[int], athlete_name: Optional[str], page_number: int, page_size: int) -> List[TournamentParticipationResponse]:
        """
        Obtiene una lista paginada de participantes en un torneo desde la base de datos.

        Args:
            tournament_id (Optional[int]): Identificador del torneo.
            athlete_name (Optional[str]): Nombre del atleta.
            page_number (int): Número de la página a obtener.
            page_size (int): Cantidad de registros por página.

        Returns:
            List[TournamentParticipationResponse]: Lista de respuestas con la información de los participantes.
        """
        participants = self.db.execute(
            text("EXEC sp_getparticipants @TournamentId = :tournament_id, @AthleteName = :athlete_name, @PageNumber = :page_number, @PageSize = :page_size"),
            {'tournament_id': tournament_id, 'athlete_name': athlete_name, 'page_number': page_number, 'page_size': page_size}
        ).fetchall()
        return [TournamentParticipationResponse.model_validate(participant) for participant in participants]

    def add_participant(self, participation: TournamentParticipationCreate) -> TournamentParticipationResponse:
        """
        Agrega un nuevo participante en un torneo en la base de datos.

        Args:
            participation (TournamentParticipationCreate): Objeto que contiene los datos de la participación a crear.

        Returns:
            TournamentParticipationResponse: Objeto que representa la participación creada.

        Raises:
            ValueError: Si la participación ya existe en la base de datos.
        """
        self.db.execute(
            text("EXEC sp_addparticipant @AthleteId = :athlete_id, @TournamentId = :tournament_id, @WeightCategoryId = :weight_category_id"),
            {'athlete_id': participation.athlete_id, 'tournament_id': participation.tournament_id, 'weight_category_id': participation.weight_category_id}
        )
        self.db.commit()
        new_participation = self.db.query(TournamentParticipation).filter_by(athlete_id=participation.athlete_id, tournament_id=participation.tournament_id).first()
        return TournamentParticipationResponse.model_validate(new_participation)