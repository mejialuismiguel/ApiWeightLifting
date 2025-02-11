from typing import List, Optional
from fastapi import Depends
from sqlalchemy.orm import Session
from app.models.weight_category import WeightCategory
from app.schemas.weight_category import WeightCategoryCreate, WeightCategoryResponse
from app.database import get_db

class WeightCategoryService:
    """
    Servicio para manejar operaciones relacionadas con categorías de peso en la base de datos.
    
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

    def get_weight_categories(self, page: int, size: int, name: Optional[str]) -> List[WeightCategoryResponse]:
        """
        Obtiene una lista paginada de categorías de peso desde la base de datos, filtrando por nombre si se proporciona.

        Args:
            page (int): Número de la página a obtener.
            size (int): Cantidad de registros por página.
            name (Optional[str]): Filtro por nombre de la categoría de peso.

        Returns:
            List[WeightCategoryResponse]: Lista de respuestas con la información de las categorías de peso.
        """
        query = self.db.query(WeightCategory)
        if name:
            query = query.filter(WeightCategory.name == name)
        query = query.order_by(WeightCategory.name)
        weight_categories = query.offset((page - 1) * size).limit(size).all()
        return [WeightCategoryResponse.model_validate(weight_category) for weight_category in weight_categories]

    def create_weight_category(self, weight_category: WeightCategoryCreate) -> WeightCategoryResponse:
        """
        Crea una nueva categoría de peso en la base de datos.

        Args:
            weight_category (WeightCategoryCreate): Objeto que contiene los datos de la categoría de peso a crear.

        Returns:
            WeightCategoryResponse: Objeto que representa la categoría de peso creada.

        Raises:
            ValueError: Si la categoría de peso ya existe en la base de datos.
        """
        db_weight_category = self.db.query(WeightCategory).filter(WeightCategory.name == weight_category.name).first()
        if db_weight_category:
            raise ValueError("La categoría de peso ya existe.")
        
        new_weight_category = WeightCategory(name=weight_category.name, min_weight=weight_category.min_weight, max_weight=weight_category.max_weight, gender=weight_category.gender)
        self.db.add(new_weight_category)
        self.db.commit()
        self.db.refresh(new_weight_category)
        return WeightCategoryResponse.model_validate(new_weight_category)