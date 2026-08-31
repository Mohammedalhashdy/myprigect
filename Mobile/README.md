# Mafqoodi Flutter Client

هذا المجلد يمثل تطبيق Flutter فقط، ومنفصل معماريًا عن Backend.

## Communication boundary
Flutter → HTTP API → ASP.NET Core 10 → CQRS → EF Core → SQL Server

لا يوجد اتصال مباشر من Flutter إلى SQL Server أو MongoDB/Firestore.

## Run
```powershell
cd Mafqoodi-Mobile
flutter pub get
flutter analyze
flutter test
flutter run
```

عنوان الـAPI يُضبط من إعدادات التطبيق/البيئة ولا تُحفظ الأسرار داخل التطبيق.
