# Mafqoodi Backend

هذا المجلد مستقل عن Flutter.

## Stack
- ASP.NET Core 10
- Clean Architecture
- CQRS + MediatR
- EF Core 10
- SQL Server
- JWT Authentication / Authorization

## Projects
- `Mafqoodi.Domain` — قواعد المجال.
- `Mafqoodi.Application` — CQRS، DTOs، Validation، Abstractions.
- `Mafqoodi.Infrastructure` — EF Core، SQL Server، Repositories، Security، Integrations.
- `Mafqoodi.API` — HTTP boundary وSwagger.

## Rule
Flutter لا يتصل بقاعدة البيانات مباشرة. الاتصال الوحيد هو HTTP API.

## Run
```powershell
dotnet restore Mafqoodi.API/Mafqoodi.API.csproj
dotnet build Mafqoodi.API/Mafqoodi.API.csproj
dotnet run --project Mafqoodi.API/Mafqoodi.API.csproj
```

ضع `DefaultConnection` و`Jwt:Key` في إعدادات البيئة/Secrets، ولا تضع الأسرار في Git.
