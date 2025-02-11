# Weightlifting Tournaments API

Welcome to the Weightlifting Tournaments API! This project is a complete implementation of an API to manage weightlifting tournaments for the Olympic Games, developed in both C# and Python. This project is part of my skill portfolio and demonstrates my ability to work with multiple programming languages and technologies.

## Spanish documentation
[Spanish](README-ES.md)

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

#### Competition Results:
- ***AthleteAttemptSummary:*** Shows the number of attempts made by a competitor within a tournament.
- ***CompetitionResult:*** Shows the result of a competition tournament which sums the best attempts in the Snatch and/or Clean and Jerk modalities, organizing the best weights from highest to lowest.

### Database

The information for implementation and deployment regarding the database design, container creation, test data, and stored procedures can be found in the data folder at the root of the directory.

In the /data/docker folder, you will find the required docker-compose to bring up a SQL Server instance. Modify it for credentials and bring up the file with: ```docker-compose up```

## Implementation Details

The implementation information is divided between the document:
- ***C# APIs:*** [implementation-c#.md](implementation-c%23.md)
- ***Python APIs:*** [implementation-python.md](implementation-python.md)

## Example Implementation Images: [/images](/images)

## Contributions

Contributions are welcome! If you wish to contribute to this project, please open an issue or send a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

---

Thank you for visiting my project! I hope you find this API useful and that it demonstrates my software development skills. If you have any questions or comments, feel free to contact me.