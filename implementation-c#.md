# Torneos de Halterofilia API - Implementación en C#

¡Bienvenido a la API de Torneos de Halterofilia! Esta implementación en C# se ha generado para poder detallar como desarrollar una API base y escalable utilizando .NET Core.

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

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (o cualquier otra base de datos compatible)

La información para implementación y despliegue sobre el diseño de la base de datos, creación de contenedor, datos de prueba y procedimientos almacenados se encuentra en la carpeta data en la raíz del directorio.

### Configuración

1. Clona el repositorio:
    ```sh
    git clone https://github.com/tu-usuario/torneos-halterofilia-api.git
    cd torneos-halterofilia-api/apis_c#/AthleteApi/AthleteApi
    ```

2. Restaura los paquetes NuGet:
    ```sh
    dotnet restore
    ```

3. Configura la cadena de conexión en `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
      },
      "Serilog": {
        "_comment_SwitchLogDBorFile": "Habilitar solo uno de los dos logs DB o Archivo se puede usar file o db",
        "_comment_EnableDBLogging": "Habilitar logs en base de datos",
        "_comment_EnableFileLogging": "Habilitar logs en archivo",
        "EnableFileLogging": false,
        "EnableDbLogging": false,
        "SwitchLogDBorFile": "db",
      }
      "Jwt": {
        "_comment":"Key de encriptacion para autenticacion JWT",
        "Key": "your_jwt_secret_key"
      },
      "Auth": {
        "_commentAuth":"username y password para api de obtencion de token JWT",
        "Username": "string",
        "Password": "string"
      }
    }
    ```

4. Ejecuta la aplicación:
    ```sh
    dotnet run
    ```

## Endpoints Principales

### Autenticación obtencion de token JWT
- **Login**: `POST /api/auth/login`

### Paises
- **Crear Pais**: `POST /api/country`
- **Obtener Pais**: `GET /api/country`

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


## Contribuciones
¡Las contribuciones son bienvenidas! Si deseas contribuir a este proyecto, por favor abre un issue o envía un pull request.

## Licencia
Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para obtener más detalles.

---

¡Gracias por visitar mi proyecto! Espero que encuentres útil esta API y que demuestre mis habilidades en desarrollo de software. Si tienes alguna pregunta o comentario, no dudes en contactarme.
