import '../../core/network/api_client.dart';
import '../../domain/entities/report.dart';

abstract interface class IReportRepository {
  Future<List<Report>> getReports({String? category, String? reportType, String? status});
  Future<Report> getReport(String id);
  Future<List<Report>> getMyReports();
  Future<Report> createReport(Map<String, dynamic> request);
}

class ReportRepository implements IReportRepository {
  ReportRepository(this._api);
  final ApiClient _api;

  String _query(Map<String, String> params) => params.isEmpty
      ? ''
      : '?${params.entries.map((e) => '${Uri.encodeQueryComponent(e.key)}=${Uri.encodeQueryComponent(e.value)}').join('&')}';

  @override
  Future<List<Report>> getReports({String? category, String? reportType, String? status}) async {
    final data = await _api.get('api/reports${_query({
      if (category != null && category.isNotEmpty) 'category': category,
      if (reportType != null && reportType.isNotEmpty) 'reportType': reportType,
      if (status != null && status.isNotEmpty) 'status': status,
    })}') as List<dynamic>;
    return data.map((e) => Report.fromJson(e as Map<String, dynamic>)).toList();
  }

  @override
  Future<Report> getReport(String id) async {
    final data = await _api.get('api/reports/$id') as Map<String, dynamic>;
    return Report.fromJson(data);
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
