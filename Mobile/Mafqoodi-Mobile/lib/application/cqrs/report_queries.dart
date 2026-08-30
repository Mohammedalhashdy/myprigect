import '../../data/repositories/report_repository.dart';
import '../../domain/entities/report.dart';

class GetReportsQuery {
  const GetReportsQuery({this.category, this.reportType, this.status});
  final String? category;
  final String? reportType;
  final String? status;
}

class GetReportsHandler {
  GetReportsHandler(this.repository);
  final IReportRepository repository;
  Future<List<Report>> execute(GetReportsQuery query) => repository.getReports(category: query.category, reportType: query.reportType, status: query.status);
}

class GetMyReportsQuery { const GetMyReportsQuery(); }
class GetMyReportsHandler {
  GetMyReportsHandler(this.repository);
  final IReportRepository repository;
  Future<List<Report>> execute(GetMyReportsQuery query) => repository.getMyReports();
}
