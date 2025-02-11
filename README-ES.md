# Torneos de Halterofilia API

¡Bienvenido a la API de Torneos de Halterofilia! Este proyecto es una implementación completa de una API para gestionar torneos de halterofilia para Juegos Olímpicos, desarrollada tanto en C# como en Python. Este proyecto forma parte de mi portafolio de habilidades y demuestra mi capacidad para trabajar con múltiples lenguajes de programación y tecnologías.

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


#### Resultados competiciones:
- ***AthleteAttemptSummary:*** Muestra el numero de intentos realizados por competidor dentro de un torneo.
- ***CompetitionResult:*** Muestra el resultado de un torneo de competicion el cual sume el mejor de los intentos en la modalidad de Arranque y/o Envión organizando de mayor a menor los mejores pesos.

### Base de Datos

La información para implementación y despliegue sobre el diseño de la base de datos, creación de contenedor, datos de prueba y procedimientos almacenados se encuentra en la carpeta data en la raíz del directorio.

En la carpeta /data/docker se encuentra el docker-compose requerido para levantar una instancia de sql server modificarlo para credenciales
levantar el archivo con: ```docker-compose up```

## Detalle de implementación

La información de implementación se encuentra dividida entre el documento:
- ***Apis c#:*** [implementation-c#.md](implementation-c%23-ES.md)
- ***Apis Python*** [implementation-python.md](implementation-python-ES.md)

## Imagenes de Ejemplo de implementacion: [/images](/images)


## Contribuciones

¡Las contribuciones son bienvenidas! Si deseas contribuir a este proyecto, por favor abre un issue o envía un pull request.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para obtener más detalles.

---

¡Gracias por visitar mi proyecto! Espero que encuentres útil esta API y que demuestre mis habilidades en desarrollo de software. Si tienes alguna pregunta o comentario, no dudes en contactarme.