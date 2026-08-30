# Migration Progress — 2026-08-31

## Current branch
`latest-local-2026-08-30-rebuilt`

## Completed in this pass
- Added server-side OTP generation and verification behind `IOtpService`.
- Added OTP CQRS commands/handlers.
- Added authenticated OTP request/verification endpoints.
- Added organization repository abstraction and EF implementation.
- Added organization CQRS read query and API endpoint.
- Kept SQL Server as the only application database boundary.
- Kept the original Mafqoodi branch untouched.

## Important implementation rule
Flutter communicates with the ASP.NET Core API. It must not access SQL Server, MongoDB, or Firestore directly.

## Remaining integration work
- Replace development OTP exposure with a production SMS provider adapter.
- Complete notifications persistence/read/unread API and push provider integration.
- Complete support conversation/message read APIs and Flutter screens.
- Complete organization management/admin workflows.
- Complete server-side Gemini adapter and secure configuration.
- Transplant all original Mafqoodi Flutter screens/assets and map each data operation to API/CQRS.
- Add unit/integration tests for auth, reports, matching, support, notifications, and admin flows.
- Run `dotnet build`, tests, Flutter analyze/test, and release builds in an environment with SDKs installed.
