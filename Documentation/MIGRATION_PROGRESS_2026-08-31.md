# Migration Progress — 2026-08-31

## Current branch
`latest-local-2026-08-30-rebuilt`

## Completed in this pass
- Added server-side OTP generation and verification behind `IOtpService`.
- Added OTP CQRS commands/handlers and authenticated endpoints.
- Added organization repository abstraction, EF implementation, CQRS read query and API endpoint.
- Added notifications persistence/domain/API pieces and admin broadcast flow.
- Added support conversation/message API and ownership enforcement.
- Expanded Flutter `Report` domain model to match the API response contract.
- Added Flutter report-detail navigation and display for category, reward, status, review status, date and publisher phone.
- Connected Flutter report type filtering to the server query instead of client-only filtering.
- Added report-detail repository operation (`GET /api/reports/{id}`).
- Kept SQL Server as the only application database boundary.
- Kept the original Mafqoodi branch untouched.

## Remaining integration work
- Replace development OTP exposure with a production SMS provider adapter.
- Complete push-notification provider integration.
- Complete organization management/admin workflows in Flutter.
- Complete support conversation/message screens in Flutter.
- Complete server-side Gemini adapter and secure configuration.
- Transplant all original Mafqoodi Flutter screens/assets and map each data operation to API/CQRS.
- Add unit/integration tests for auth, reports, matching, support, notifications, and admin flows.
- Run `dotnet build`, tests, Flutter analyze/test, and release builds in an environment with SDKs installed.
