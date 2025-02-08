from fastapi import FastAPI
from app.routers import auth, athlete, tournament, country
from app.utils.logger import setup_logger
from app.database import engine, Base
from app.models import log

# Crear una instancia de la aplicación FastAPI
app = FastAPI(
    openapi_url="/openapi.json",
    docs_url="/docs",
    redoc_url="/redoc"
)

# Configurar el logger
setup_logger()

# Crear las tablas en la base de datos
Base.metadata.create_all(bind=engine)

# Incluir los routers para las rutas de la API
app.include_router(auth.router)
app.include_router(country.router)
app.include_router(athlete.router)
app.include_router(tournament.router)

@app.get("/")
def read_root():
    """
    Ruta raíz que retorna un mensaje de bienvenida.
    
    Returns:
        dict: Un diccionario con un mensaje de bienvenida.
    """
    return {"message": "Welcome to the Weightlifting API"}

# #Imprimir la configuración del router después de incluirlo (comentado para producción)
# print("*****************")
# for route in app.routes:
#    print(f"Path: {route.path}, Name: {route.name}, Methods: {route.methods}")
