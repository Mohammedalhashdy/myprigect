# Mafqoodi Rebuilt Architecture

## Source of truth
- Business domain: Mafqoodi `latest-local-2026-08-30`.
- Architectural reference: Rawaa + Rawaa-Mobile CQRS.

## Final flow
Flutter → HTTP API → Application/CQRS → Domain → Infrastructure/EF Core → SQL Server.

MVC Dashboard → HTTP API → Application/CQRS → Domain → Infrastructure/EF Core → SQL Server.

## Projects
- `Backend/Mafqoodi.Domain`: entities and domain concepts.
- `Backend/Mafqoodi.Application`: DTOs, CQRS, validation, abstractions.
- `Backend/Mafqoodi.Infrastructure`: EF Core, SQL Server, repositories, security.
- `Backend/Mafqoodi.API`: REST API, JWT, Swagger, middleware.
- `Dashboard/Mafqoodi.Dashboard`: separate ASP.NET Core MVC administration UI.
- `Mobile/Mafqoodi-Mobile`: Flutter + Riverpod + CQRS + HTTP client.

## Database
SQL Server is the only primary application database. MongoDB and Firestore are not used by the rebuilt application flow.

## Security
Passwords are hashed, JWT is issued by the API, authorization is enforced server-side, and Flutter stores only the access token/session identifiers in secure storage.

## Important migration note
The original UI/business implementation is preserved in the original Mafqoodi repository and safety branch. This isolated workspace currently contains the new architecture and representative migrated Mafqoodi workflows; binary asset transplantation requires repository/file transfer support before the original image/font binaries can be copied here.
