from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from app.config import settings

# Obtener la URL de la base de datos desde las configuraciones
SQLALCHEMY_DATABASE_URL = settings.DATABASE_URL

# Crear el motor de la base de datos
engine = create_engine(SQLALCHEMY_DATABASE_URL)

# Configurar la sesión de la base de datos
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

# Crear una clase base para los modelos declarativos
Base = declarative_base()

def get_db():
    """
    Proveedor de dependencias para obtener una sesión de la base de datos.
    
    Yields:
        db (Session): Sesión de la base de datos.
    """
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()
