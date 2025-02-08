from typing import List, Optional
from fastapi import Depends
from sqlalchemy.orm import Session
from app.models.tournament import Tournament
from app.schemas.tournament import TournamentCreate, TournamentResponse
from app.database import get_db

class TournamentService:
    """
    Servicio para manejar operaciones relacionadas con torneos en la base de datos.
    
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

    def get_tournaments(self, page: int, size: int, id: Optional[int], name: Optional[str]) -> List[TournamentResponse]:
        """
        Obtiene una lista paginada de torneos desde la base de datos, filtrando por ID y nombre si se proporcionan.

        Args:
            page (int): Número de la página a obtener.
            size (int): Cantidad de registros por página.
            id (Optional[int]): Filtro por ID del torneo.
            name (Optional[str]): Filtro por nombre del torneo.

        Returns:
            List[TournamentResponse]: Lista de respuestas con la información de los torneos.
        """
        query = self.db.query(Tournament)
        if id:
            query = query.filter(Tournament.id == id)
        if name:
            query = query.filter(Tournament.name == name)
        query = query.order_by(Tournament.id)
        tournaments = query.offset((page - 1) * size).limit(size).all()
        return [TournamentResponse.from_orm(tournament) for tournament in tournaments]

    def create_tournament(self, tournament: TournamentCreate) -> TournamentResponse:
        """
        Crea un nuevo torneo en la base de datos.

        Args:
            tournament (TournamentCreate): Datos del torneo a crear.

        Returns:
            TournamentResponse: Respuesta con la información del torneo creado.
        """
        db_tournament = Tournament(**tournament.dict())
        self.db.add(db_tournament)
        self.db.commit()
        self.db.refresh(db_tournament)
        return TournamentResponse.from_orm(db_tournament)
