# SQL Server Database

Primary database: SQL Server.

## Development
Set the API connection string with `ConnectionStrings__DefaultConnection` when deploying, or edit `Backend/Mafqoodi.API/appsettings.json` for local development.

Set `Jwt__Key` to a long random secret (never commit production secrets).

## EF Core
From `Backend/Mafqoodi.API`:

```bash
dotnet ef migrations add InitialCreate --project ../Mafqoodi.Infrastructure --startup-project .
dotnet ef database update --project ../Mafqoodi.Infrastructure --startup-project .
```

The EF model in `Mafqoodi.Infrastructure/Persistence/ApplicationDbContext.cs` is the authoritative schema definition.
