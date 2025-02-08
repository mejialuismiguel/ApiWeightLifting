# Importa List y Optional del módulo typing para definir tipos de datos más precisos.
from typing import List, Optional  
# Importa Depends y HTTPException de FastAPI para manejar dependencias y excepciones HTTP.
from fastapi import Depends, HTTPException  
# Importa Session de SQLAlchemy para manejar sesiones de la base de datos.
from sqlalchemy.orm import Session  
# Importa el modelo Athlete desde el módulo models.athlete de la aplicación.
from app.models.athlete import Athlete  
# Importa los esquemas AthleteCreate y AthleteResponse desde el módulo schemas.athlete de la aplicación.
from app.schemas.athlete import AthleteCreate, AthleteResponse  
# Importa la función get_db desde el módulo database de la aplicación para obtener una sesión de la base de datos.
from app.database import get_db  

class AthleteService:
    """
    Servicio para manejar operaciones relacionadas con atletas en la base de datos.
    
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

    def get_athletes(self, page: int, size: int, dni: Optional[str], first_name: Optional[str], last_name: Optional[str]) -> List[AthleteResponse]:
        """
        Obtiene una lista paginada de atletas desde la base de datos, filtrando por DNI, nombre y apellido si se proporcionan.

        Args:
            page (int): Número de la página a obtener.
            size (int): Cantidad de registros por página.
            dni (Optional[str]): Filtro por DNI del atleta.
            first_name (Optional[str]): Filtro por nombre del atleta.
            last_name (Optional[str]): Filtro por apellido del atleta.

        Returns:
            List[AthleteResponse]: Lista de respuestas con la información de los atletas.
        """
        query = self.db.query(Athlete)
        if dni:
            query = query.filter(Athlete.dni == dni)
        if first_name:
            query = query.filter(Athlete.first_name == first_name)
        if last_name:
            query = query.filter(Athlete.last_name == last_name)
        query = query.order_by(Athlete.id)  # Añadir cláusula ORDER BY
        athletes = query.offset((page - 1) * size).limit(size).all() # Aplicar paginación con offset y limit
        return [AthleteResponse.from_orm(athlete) for athlete in athletes]

    def create_athlete(self, athlete: AthleteCreate) -> AthleteResponse:
        """
        Crea un nuevo atleta en la base de datos.

        Args:
            athlete (AthleteCreate): Datos del atleta a crear.

        Returns:
            AthleteResponse: Respuesta con la información del atleta creado.
        """
        
        db_athlete = Athlete(**athlete.dict())  # Crear una instancia del modelo Athlete con los datos proporcionados
        self.db.add(db_athlete)  # Añadir la instancia del atleta a la sesión de la base de datos
        self.db.commit()  # Confirmar la transacción para guardar el atleta en la base de datos
        self.db.refresh(db_athlete)  # Refrescar la instancia del atleta para obtener los datos actualizados desde la base de datos
        return AthleteResponse.from_orm(db_athlete)  # Devolver una respuesta con la información del atleta creado
