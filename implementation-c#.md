# Weightlifting Tournaments API - C# Implementation

Welcome to the Weightlifting Tournaments API! This C# implementation has been generated to detail how to develop a base and scalable API using .NET Core.

## Project Description

The Weightlifting Tournaments API allows managing athletes, tournaments, attempts, and competition results. The API is designed to be robust and scalable, using best software development practices.

### Main Features

- **Documentation**: Detailed documentation in Swagger.
- **Authentication and Authorization**: Implementation of JWT authentication to secure the API.
- **JWT Token Generation API**: Obtainable via username and password.
- **Country Management**: Create, Read for countries which are an input for athletes.
- **Weight Category Management**: Create, Read for weight categories which are an input for athletes.
- **Athlete Management**: Create, Read for athletes.
- **Tournament Management**: Create, Read for tournaments.
- **Athlete Participation Management in Tournaments**: Create, Read for athlete participation in tournaments.
- **Attempt Management**: Create, Read for weightlifting attempts by participation in tournaments.
- **Competition Results**: Obtain competition results with pagination and filters.

## Configuration and Execution

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any other compatible database)

The information for implementation and deployment regarding the database design, container creation, test data, and stored procedures can be found in the data folder at the root of the directory.

In the /data/docker folder, you will find the required docker-compose to bring up a SQL Server instance. Modify it for credentials and bring up the file with: ```docker-compose up```

### Configuration

1. Clone the repository:
    ```sh
    git clone https://github.com/your-username/weightlifting-tournaments-api.git
    cd weightlifting-tournaments-api/apis_c#/AthleteApi/AthleteApi
    ```

2. Restore the NuGet packages:
    ```sh
    dotnet restore
    ```

3. Configure the connection string in `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
      },
      "Serilog": {
        "_comment_SwitchLogDBorFile": "Enable only one of the two logs DB or File, you can use file or db",
        "_comment_EnableDBLogging": "Enable database logging",
        "_comment_EnableFileLogging": "Enable file logging",
        "EnableFileLogging": false,
        "EnableDbLogging": false,
        "SwitchLogDBorFile": "db"
      },
      "Jwt": {
        "_comment": "Encryption key for JWT authentication",
        "Key": "your_jwt_secret_key"
      },
      "Auth": {
        "_commentAuth": "username and password for JWT token generation API",
        "Username": "string",
        "Password": "string"
      }
    }
    ```

4. Run the application:
    ```sh
    dotnet run
    ```

## Endpoints

### Authentication for obtaining JWT token
- **Login**: `POST /api/Auth/login`

### Countries
- **Create Country**: `POST /api/Country`
- **Get Country**: `GET /api/Country`

### Weight Categories
- **Create Weight Category**: `POST /api/WeightCategory`
- **Get Weight Categories**: `GET /api/WeightCategory`

### Athletes
- **Create Athlete**: `POST /api/Athlete`
- **Get Athletes**: `GET /api/Athlete`

### Tournaments
- **Create Tournament**: `POST /api/Tournament`
- **Get Tournaments**: `GET /api/Tournament`

### Tournament Participation
- **Add Participant to Tournament**: `POST /api/TournamentParticipation`
- **Review Tournament Participants**: `GET /api/TournamentParticipation`

### Attempts
- **Add Attempt by Type and Participant**: `POST /api/Attempt`
- **Get Attempts by Tournament**: `GET /api/Attempt`

### Competition Results
- **Get Results of Attempts Made by Athlete**: `GET /api/AthleteAttemptSummary`
- **Get Competition Results with Best Weights**: `GET /api/CompetitionResult`

## Contributions
Contributions are welcome! If you wish to contribute to this project, please open an issue or send a pull request.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

---

Thank you for visiting my project! I hope you find this API useful and that it demonstrates my software development skills. If you have any questions or comments, feel free to contact me.