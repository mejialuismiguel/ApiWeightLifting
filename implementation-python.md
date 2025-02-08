# Torneos de Halterofilia API - Implementación en Python

¡Bienvenido a la API de Torneos de Halterofilia! Esta implementación en Python para tener una base de como desarrollar API's robustas y escalables utilizando FastAPI.

## Descripción del Proyecto

La API de Torneos de Halterofilia permite gestionar atletas, torneos, intentos y resultados de competencias. La API está diseñada para ser robusta y escalable, utilizando las mejores prácticas de desarrollo de software.

### Características Principales

- **Gestión de Atletas**: CRUD (Crear, Leer, Actualizar, Eliminar) para atletas.
- **Gestión de Torneos**: CRUD para torneos.
- **Gestión de Intentos**: CRUD para intentos de levantamiento de pesas.
- **Resultados de Competencias**: Obtener resultados de competencias con paginación y filtros.
- **Autenticación y Autorización**: Implementación de autenticación JWT para asegurar la API.

## Configuración y Ejecución

### Requisitos Previos

- [Python 3.8+](https://www.python.org/downloads/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (o cualquier otra base de datos compatible)

La información para implementación y despliegue sobre el diseño de la base de datos, creación de contenedor, datos de prueba y procedimientos almacenados se encuentra en la carpeta data en la raíz del directorio.

### Configuración

1. Clona el repositorio:
    ```sh
    git clone https://github.com/tu-usuario/torneos-halterofilia-api.git
    cd torneos-halterofilia-api/apis_py
    ```

2. Crea y activa un entorno virtual:
    ```sh
    python -m venv venv
    source venv/bin/activate  # En Windows: venv\Scripts\activate
    ```

3. Instala las dependencias:
    ```sh
    pip install -r requirements.txt
    ```

4. Configura la cadena de conexión en `config.py`:
    ```python
    DATABASE_URL: str = os.getenv("DATABASE_URL", "mssql+pyodbc://user:password@server/weightlifting?driver=ODBC+Driver+17+for+SQL+Server")
    JWT_SECRET: str = os.getenv("JWT_SECRET", "AquiTuKeyParaEncritar")
    JWT_ALGORITHM: str = os.getenv("JWT_ALGORITHM", "HS256")
    JWT_EXPIRATION_TIME: int = os.getenv("JWT_EXPIRATION_TIME", 3600)
    USERNAME: str = os.getenv("USERNAME", "UsuarioPrueba")
    PASSWORD: str = os.getenv("PASSWORD", "yourSecurePassword123")
    ```

5. Ejecuta la aplicación:
    ```sh
    uvicorn app.main:app --reload
    ```

## Endpoints Principales

### Atletas

- **Crear Atleta**: `POST /api/athlete`
- **Obtener Atletas**: `GET /api/athlete`

### Torneos

- **Crear Torneo**: `POST /api/tournament`
- **Obtener Torneos**: `GET /api/tournament`

### Intentos

- **Agregar Intento**: `POST /api/attempt`
- **Obtener Intentos por Torneo**: `GET /api/attempt`

### Resultados de Competencias

- **Obtener Resultados**: `GET /api/competitionresult`

### Autenticación

- **Login**: `POST /api/auth/login`

## Contribuciones

¡Las contribuciones son bienvenidas! Si deseas contribuir a este proyecto, por favor abre un issue o envía un pull request.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para obtener más detalles.

---

¡Gracias por visitar mi proyecto! Espero que encuentres útil esta API y que demuestre mis habilidades en desarrollo de software. Si tienes alguna pregunta o comentario, no dudes en contactarme.