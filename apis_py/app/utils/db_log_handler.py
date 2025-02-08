import logging
from sqlalchemy.orm import Session
from app.database import SessionLocal
from app.models.log import Log

class DBLogHandler(logging.Handler):
    """
    Manejador de registro personalizado que guarda registros en una base de datos.
    
    Este manejador de registro extiende logging.Handler y guarda los registros en una tabla de base de datos usando SQLAlchemy.
    """

    def __init__(self):
        """
        Inicializa el manejador de registro.
        """
        logging.Handler.__init__(self)

    def emit(self, record):
        """
        Emite un registro guardándolo en la base de datos.
        
        Args:
            record (logging.LogRecord): El registro de log que se va a guardar.
        """
        log_entry = self.format(record)
        db: Session = SessionLocal()
        db_log = Log(
            name=record.name,
            level=record.levelname,
            message=log_entry
        )
        db.add(db_log)
        db.commit()
        db.close()
