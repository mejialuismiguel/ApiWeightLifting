from typing import List, Optional
from fastapi import Depends
from sqlalchemy.orm import Session
from app.models.country import Country
from app.schemas.country import CountryResponse, CountryCreate
from app.database import get_db

class CountryService:
    """
    Servicio para manejar operaciones relacionadas con países en la base de datos.
    
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

    def get_countries(self, page: int, size: int, id: Optional[int], name: Optional[str]) -> List[CountryResponse]:
        """
        Obtiene una lista paginada de países desde la base de datos, filtrando por ID y nombre si se proporcionan.

        Args:
            page (int): Número de la página a obtener.
            size (int): Cantidad de registros por página.
            id (Optional[int]): Filtro por ID del país.
            name (Optional[str]): Filtro por nombre del país.

        Returns:
            List[CountryResponse]: Lista de respuestas con la información de los países.
        """
        query = self.db.query(Country)
        if id:
            query = query.filter(Country.id == id)
        if name:
            query = query.filter(Country.name == name)
        query = query.order_by(Country.name)
        countries = query.offset((page - 1) * size).limit(size).all()
        return [CountryResponse.model_validate(country) for country in countries]

    def create_country(self, country: CountryCreate) -> CountryResponse:
        """
        Crea un nuevo país en la base de datos.

        Args:
            country (CountryCreate): Objeto que contiene los datos del país a crear.

        Returns:
            CountryResponse: Objeto que representa el país creado.

        Raises:
            ValueError: Si el país ya existe en la base de datos.
        """
        db_country = self.db.query(Country).filter(Country.name == country.name).first()
        if db_country:
            raise ValueError("El país ya existe.")
        
        new_country = Country(name=country.name, code=country.code)
        self.db.add(new_country)
        self.db.commit()
        self.db.refresh(new_country)
        return CountryResponse.model_validate(new_country)