import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import '../../core/network/api_client.dart';
import '../../data/repositories/auth_repository.dart';
import '../../data/repositories/report_repository.dart';
import '../../application/cqrs/auth_commands.dart';
import '../../application/cqrs/report_queries.dart';
import '../../domain/entities/report.dart';

final secureStorageProvider = Provider((ref) => const FlutterSecureStorage());
final apiClientProvider = Provider((ref) => ApiClient(ref.read(secureStorageProvider)));
final authRepositoryProvider = Provider<IAuthRepository>((ref) => AuthRepository(ref.read(apiClientProvider), ref.read(secureStorageProvider)));
final reportRepositoryProvider = Provider<IReportRepository>((ref) => ReportRepository(ref.read(apiClientProvider)));

final authSessionProvider = StateProvider<AuthSession?>((ref) => null);

final reportsProvider = FutureProvider.autoDispose<List<Report>>((ref) {
  return GetReportsHandler(ref.read(reportRepositoryProvider)).execute(const GetReportsQuery(status: 'active'));
});

final myReportsProvider = FutureProvider.autoDispose<List<Report>>((ref) {
  return GetMyReportsHandler(ref.read(reportRepositoryProvider)).execute(const GetMyReportsQuery());
});
