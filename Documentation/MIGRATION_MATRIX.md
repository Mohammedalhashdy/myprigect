# Mafqoodi Migration Matrix

| Existing Mafqoodi capability | Rebuilt target | Status |
|---|---|---|
| User | Domain + SQL Server + API | Implemented |
| Reports | Domain + EF Core + CQRS + API | Implemented |
| My Reports | Flutter Query + API | Implemented |
| Authentication | JWT API + secure Flutter storage | Implemented |
| Password encryption | Password hashing | Replaced |
| Admin statistics | CQRS query + MVC | Implemented |
| User ban/role | Admin command + MVC | Implemented |
| Support | SQL model + API command | Partial |
| Organizations | SQL model | Partial |
| Notifications | SQL model | Partial |
| Smart Matching | Server geospatial baseline | Partial |
| Gemini semantic matching | Server-side provider boundary | Requires configuration/provider implementation |
| OTP | Backend boundary required | Requires SMS provider |
| Firebase direct access | Removed from rebuilt flow | Target architecture |
| MongoDB direct access | Removed from rebuilt flow | Target architecture |
| Flutter Provider | Riverpod | Rebuilt shell |
| Flutter CQRS | Commands/Queries/Handlers | Implemented for auth/reports |
| MVC Dashboard | Separate ASP.NET Core MVC | Implemented baseline |
| Swagger | API | Implemented |
| Postman | Collection | Implemented |
| SQL Server | EF Core + migration + schema script | Implemented |
| Original Flutter visual assets | Existing source | Requires binary asset transfer |

This matrix deliberately distinguishes implemented work from blocked/partial work; no unsupported feature is marked complete.
