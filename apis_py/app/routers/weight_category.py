from fastapi import APIRouter, Depends, Query, HTTPException, status
from typing import List, Optional
from app.schemas.weight_category import WeightCategoryResponse, WeightCategoryCreate
from app.services.weight_category_service import WeightCategoryService
from app.auth.jwt_bearer import JWTBearer

router = APIRouter(
    prefix="/WeightCategory",
    tags=["WeightCategory"],
    dependencies=[Depends(JWTBearer())]
)

@router.get("/", response_model=List[WeightCategoryResponse])
def get_weight_categories(
    page: int = Query(1, title="Número de página", description="Número de la página a consultar", ge=1),
    size: int = Query(10, title="Tamaño de página", description="Cantidad de categorías de peso por página", ge=1),
    name: Optional[str] = Query(None, title="Nombre de la categoría de peso", description="Filtrar por nombre de la categoría de peso"),
    service: WeightCategoryService = Depends()
):
    """
    Obtiene una lista paginada de categorías de peso registradas en el sistema.

    Parámetros:
        - **page (int)**: Número de la página a consultar (mínimo 1, por defecto 1).
        - **size (int)**: Cantidad de categorías de peso a devolver por página (mínimo 1, por defecto 10).
        - **name (str, opcional)**: Filtra las categorías de peso por su nombre.

    Retorna:
        - **List[WeightCategoryResponse]**: Lista de categorías de peso registradas en la página solicitada.

    Excepciones:
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para acceder a la información.
    """
    return service.get_weight_categories(page, size, name)

@router.post("/", response_model=WeightCategoryResponse, status_code=status.HTTP_201_CREATED)
def create_weight_category(
    weight_category: WeightCategoryCreate,
    service: WeightCategoryService = Depends()
):
    """
    Crea una nueva categoría de peso en el sistema.

    Parámetros:
        - **weight_category (WeightCategoryCreate)**: Objeto que contiene los datos de la categoría de peso a crear.

    Retorna:
        - **WeightCategoryResponse**: Objeto que representa la categoría de peso creada.

    Excepciones:
        - **HTTPException 400**: Si la categoría de peso ya existe o los datos son inválidos.
        - **HTTPException 401**: Si el usuario no está autenticado.
        - **HTTPException 403**: Si el usuario no tiene permisos para crear una categoría de peso.
    """
    try:
        return service.create_weight_category(weight_category)
    except ValueError as e:
        raise HTTPException(status_code=status.HTTP_400_BAD_REQUEST, detail=str(e))