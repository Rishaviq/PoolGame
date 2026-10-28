
# Pool Game API

This project is the beginning of a full web application designed to help me and my friends track our pool games. Have you ever wondered whether you’ve won more games than a friend? This app aims to solve that problem by storing and retrieving game statistics in a database. Currently, this repository contains only the backend, while a separate project will handle the frontend visualization.

## Demo

You can try the site using the Docker Compose demo in `WholeAppDemo.yaml`. From the repo folder, run `docker compose -f WholeAppDemo.yaml up -d`, then visit [https://localhost:8443](https://localhost:8443). The home page has a small bug when a game has no saved stats. To work around it, create a game on the site and save stats for it. Since this is only a demo and there is no actual game being played, this does not affect the demo in any meaningful way. When you are done, run `docker compose -f WholeAppDemo.yaml down -v` to stop the demo and remove its database.




## My Role & Responsibilities
I designed and implemented the backend end-to-end, including:

- Relational data model and entity relationships
- REST API design and routing
- Service and repository layers
- Validation and error handling
- Dependency injection and application structure
- Environment-based configuration and Docker support
## Architecture Overview
The application follows a layered architecture:

- Controllers handle HTTP concerns only
- Services contain business logic
- Repositories handle data access
- Models represent the domain

This separation keeps business logic independent from transport and persistence concerns.
## Data Model
The data model is relational and designed to reflect the domain:

- Users represent players
- Games represent individual matches
- Statistics are tracked per player per game
## Error Handling

Errors are handled centrally via middleware to ensure consistent responses.
The API distinguishes between validation errors, authorization failures, and server errors, avoiding leaking internal exceptions to clients.
## Production Considerations
The project was structured with production use in mind:

- Environment-based configuration
- Docker support
- Clear separation of concerns
- Extensible architecture for future features
## Tech Stack

- ASP.NET Core
- C#
- Microsoft SQL Server
- Docker
