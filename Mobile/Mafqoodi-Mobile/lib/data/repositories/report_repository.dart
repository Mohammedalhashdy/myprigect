import '../../core/network/api_client.dart';
import '../../domain/entities/report.dart';

abstract interface class IReportRepository {
  Future<List<Report>> getReports({String? category, String? reportType, String? status});
  Future<List<Report>> getMyReports();
  Future<Report> createReport(Map<String, dynamic> request);
}

class ReportRepository implements IReportRepository {
  ReportRepository(this._api);
  final ApiClient _api;

  @override
  Future<List<Report>> getReports({String? category, String? reportType, String? status}) async {
    final params = <String, String>{if (category != null) 'category': category, if (reportType != null) 'reportType': reportType, if (status != null) 'status': status};
    final query = params.entries.map((e) => '${Uri.encodeQueryComponent(e.key)}=${Uri.encodeQueryComponent(e.value)}').join('&');
    final data = await _api.get('api/reports${query.isEmpty ? '' : '?$query'}') as List<dynamic>;
    return data.map((e) => Report.fromJson(e as Map<String, dynamic>)).toList();
  }

  @override
  Future<List<Report>> getMyReports() async {
    final data = await _api.get('api/reports/mine') as List<dynamic>;
    return data.map((e) => Report.fromJson(e as Map<String, dynamic>)).toList();
  }

  @override
  Future<Report> createReport(Map<String, dynamic> request) async {
    final data = await _api.post('api/reports', request) as Map<String, dynamic>;
    return Report.fromJson(data);
  }
}
