# Torneos de Halterofilia API - Implementación en Python

¡Bienvenido a la API de Torneos de Halterofilia! Esta implementación en Python para tener una base de como desarrollar API's robustas y escalables utilizando FastAPI.

## Descripción del Proyecto

La API de Torneos de Halterofilia permite gestionar atletas, torneos, intentos y resultados de competencias. La API está diseñada para ser robusta y escalable, utilizando las mejores prácticas de desarrollo de software.

### Características Principales

- **Documentación**: Documentación detallada en Swagger.
- **Autenticación y Autorización**: Implementación de autenticación JWT para asegurar la API.
- **API de Generacion de token JWT**: Obtención mediante usuario y contraseña
- **Gestión de Países**: Crear, Leer para países que es un insumo de los atletas.
- **Gestión de Categorías de peso**: Crear, Leer para categorías de peso que es un insumo de los atletas.
- **Gestión de Atletas**: Crear, Leer para atletas.
- **Gestión de Torneos**: Crear, Leer para torneos.
- **Gestión de participación de Atletas en torneos**: Crear, Leer para participación de atletas en torneos.
- **Gestión de Intentos**: Crear, Leer para intentos de levantamiento de pesas por participación en torneo.
- **Resultados de Competencias**: Obtener resultados de competencias con paginación y filtros.


## Configuración y Ejecución

### Requisitos Previos

- [Python 3.8+](https://www.python.org/downloads/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (o cualquier otra base de datos compatible)

La información para implementación y despliegue sobre el diseño de la base de datos, creación de contenedor, datos de prueba y procedimientos almacenados se encuentra en la carpeta data en la raíz del directorio.

En la carpeta /data/docker se encuentra el docker-compose requerido para levantar una instancia de sql server modificarlo para credenciales
levantar el archivo con: ```docker-compose up```


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

## Endpoints

### Autenticación obtencion de token JWT
- **Login**: `POST /api/Auth/login`

### Paises
- **Crear Pais**: `POST /api/Country`
- **Obtener Pais**: `GET /api/Country`

### Categorias de peso
- **Crear categoria de peso**: `POST /api/WeightCategory`
- **Obtener categorias de peso**: `GET /api/WeightCategory`

### Atletas
- **Crear Atleta**: `POST /api/Athlete`
- **Obtener Atletas**: `GET /api/Athlete`

### Torneos
- **Crear Torneo**: `POST /api/Tournament`
- **Obtener Torneos**: `GET /api/Tournament`

### Participacion en Torneos
- **Agregar participante a torneo**: `POST /api/TournamentParticipation`
- **Revisar participantes de torneo**: `GET /api/TournamentParticipation`

### Intentos
- **Agregar Intento por tipo y participante**: `POST /api/Attempt`
- **Obtener Intentos por Torneo**: `GET /api/Attempt`

### Resultados de Competencias
- **Obtener Resultados intentos realizados por atleta**: `GET /api/AthleteAttemptSummary`
- **Obtener Resultados ppantalla de mejores pesos**: `GET /api/CompetitionResult`

## Contribuciones

¡Las contribuciones son bienvenidas! Si deseas contribuir a este proyecto, por favor abre un issue o envía un pull request.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para obtener más detalles.

---

¡Gracias por visitar mi proyecto! Espero que encuentres útil esta API y que demuestre mis habilidades en desarrollo de software. Si tienes alguna pregunta o comentario, no dudes en contactarme.