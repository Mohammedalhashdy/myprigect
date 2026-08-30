# Mafqoodi — Final Migration Gap Matrix

> الهدف: نقل وظائف النسخة `latest-local-2026-08-30` إلى Full-Stack مع الحفاظ على الوظائف والتصميم وتحسينهما فقط.

| Feature | Flutter original | New API/CQRS | SQL Server | Migration state |
|---|---|---|---|---|
| Authentication | نعم | نعم | نعم | Core migrated |
| OTP | Mock | Server-side | لا يحتاج جدولًا في النسخة الحالية | Provider adapter pending |
| Reports | نعم | نعم | نعم | Core migrated |
| Report details | نعم | نعم | نعم | Migrated |
| My Reports | نعم | نعم | نعم | Migrated |
| Admin users | نعم | نعم | نعم | Core migrated |
| Admin reports | نعم | نعم | نعم | Core migrated |
| Dashboard statistics | نعم | نعم | نعم | Core migrated |
| Support chat | نعم | نعم | نعم | API layer in progress |
| Notifications | نعم | نعم | نعم | API layer in progress |
| Organizations | نعم | نعم | نعم | API layer added |
| Smart Matching | نعم | نعم | نعم | Server baseline added |
| Profile | نعم | API contract | نعم | Flutter parity pending |
| Settings | نعم | API/config | N/A | UI parity pending |
| Poster | نعم | Report API | نعم | UI parity pending |
| Image upload | نعم | DTO field | نعم | Storage adapter pending |
| Geolocation | نعم | Report coordinates | نعم | API mapping pending |
| Firebase legacy paths | نعم | يجب إزالتها من runtime | N/A | Decommission pending |
| MongoDB direct access | نعم | ممنوع في final client | N/A | Decommission pending |
| MVC dashboard | غير موجود | نعم | نعم | Implemented baseline |
| Tests | جزئي | جزئي | جزئي | Expansion required |

## Non-regression rule
No original user-facing feature is to be removed. UI changes must preserve the existing Mafqoodi visual language and improve responsiveness, accessibility, validation, and states where possible.

## Final verification gate
The migration is not considered production-ready until `dotnet build`, backend tests, Flutter `analyze/test`, API integration tests, and release builds pass in an environment containing the required SDKs.
