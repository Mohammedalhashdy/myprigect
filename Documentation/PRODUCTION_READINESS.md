# Mafqoodi — Production Readiness Checklist

This checklist is the final gate for the rebuilt branch and does not modify the original `latest-local-2026-08-30` branch.

## Architecture
- [x] ASP.NET Core 10 API
- [x] Clean Architecture split
- [x] CQRS/MediatR
- [x] Repository abstractions
- [x] EF Core 10 + SQL Server
- [x] JWT authentication and role authorization
- [x] MVC administration shell
- [x] Flutter API client / Riverpod migration shell

## Functional parity
- [x] Authentication baseline
- [x] Reports CRUD baseline
- [x] Report details and ownership rules
- [x] Admin statistics and user moderation baseline
- [x] Support API baseline
- [x] Notifications API baseline
- [x] Organizations read API baseline
- [x] OTP server-side baseline
- [x] Smart-matching server boundary
- [ ] Full Flutter screen-for-screen parity
- [ ] Complete image upload/storage provider
- [ ] Complete push notification provider
- [ ] Complete real SMS provider
- [ ] Complete Gemini provider
- [ ] Legacy MongoDB/Firestore/Firebase runtime removal

## Verification
- [ ] `dotnet restore`
- [ ] `dotnet build`
- [ ] `dotnet test`
- [ ] `flutter pub get`
- [ ] `flutter analyze`
- [ ] `flutter test`
- [ ] API integration tests
- [ ] MVC dashboard runtime test
- [ ] Android release build
- [ ] Windows release build
- [ ] End-to-end smoke test

A check is marked only when the corresponding source implementation exists. Runtime checks remain open until executed by CI or a development environment containing the required SDKs.
