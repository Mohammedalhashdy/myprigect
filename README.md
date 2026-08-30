# Mafqoodi Rebuilt

Professional full-stack rebuild of the Mafqoodi application based on the architecture of Rawaa / Rawaa-Mobile-CQRS while preserving Mafqoodi as the business domain.

## Stack
- ASP.NET Core 10 Web API
- Clean Architecture: Domain / Application / Infrastructure / API
- CQRS + MediatR
- FluentValidation
- EF Core + SQL Server
- JWT + server-side authorization + secure password hashing
- Swagger/OpenAPI + Postman
- Separate ASP.NET Core MVC Dashboard
- Flutter + Riverpod + Flutter CQRS + centralized HTTP client

## Structure
```text
Backend/
  Mafqoodi.Domain/
  Mafqoodi.Application/
  Mafqoodi.Infrastructure/
  Mafqoodi.API/
Dashboard/
  Mafqoodi.Dashboard/
Mobile/
  Mafqoodi-Mobile/
Database/
Documentation/
Postman/
Tests/
```

## Safety
- Original source: `Mohammedalhashdy/Mafqoodi`
- Original branch: `latest-local-2026-08-30`
- Original commit: `22576fb7d248726794df69a01f68204ade411288`
- Safety checkpoint in original repository: `backup-before-rebuild`
- Isolated rebuild branch: `latest-local-2026-08-30-rebuilt`

## Configuration
Set `Jwt__Key` and a SQL Server connection string in the deployment environment. Optional development admin seeding uses `MAFQOODI_ADMIN_EMAIL` and `MAFQOODI_ADMIN_PASSWORD`.

## Database
The authoritative EF model is in `ApplicationDbContext`. An initial migration and SQL Server schema script are included.

## Verification
Use the included GitHub Actions workflow for restore/build/test checks. The current development environment did not contain `dotnet` or `flutter`, so no local build result is claimed.

See `Documentation/IMPLEMENTATION_STATUS.md` for the exact completion and blocked-item matrix.
