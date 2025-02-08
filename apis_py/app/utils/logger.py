import logging
from app.utils.db_log_handler import DBLogHandler

def setup_logger():
    """
    Configura el logger de la aplicación para registrar mensajes en múltiples destinos.
    
    Configura el nivel de registro, el formato y los manejadores de registro:
    - StreamHandler: Imprime los mensajes de registro en la consola.
    - FileHandler: Guarda los mensajes de registro en un archivo llamado 'app.log'.
    - DBLogHandler: Guarda los mensajes de registro en una base de datos.
    """
    logging.basicConfig(
        level=logging.INFO,
        format="%(asctime)s - %(name)s - %(levelname)s - %(message)s",
        handlers=[
            logging.StreamHandler(),  # Imprimir en la consola
            logging.FileHandler("app.log"),  # Guardar en un archivo
            DBLogHandler()  # Guardar en la base de datos
        ]
    )
