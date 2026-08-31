import 'dart:async';
import 'dart:convert';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class ApiException implements Exception {
  final int statusCode;
  final String message;
  const ApiException(this.statusCode, this.message);
  @override
  String toString() => 'ApiException($statusCode): $message';
}

class ApiClient {
  ApiClient(this._storage, {String? baseUrl})
      : baseUrl = (baseUrl ?? const String.fromEnvironment('API_BASE_URL', defaultValue: 'https://10.0.2.2:7001')).replaceFirst(RegExp(r'/$'), '');

  final FlutterSecureStorage _storage;
  final String baseUrl;
  static const _timeout = Duration(seconds: 20);

  Future<Map<String, String>> _headers() async {
    final token = await _storage.read(key: 'access_token');
    return {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      if (token != null && token.isNotEmpty) 'Authorization': 'Bearer $token',
    };
  }

  Future<dynamic> _send(Future<http.Response> Function(Map<String, String>) request) async {
    late http.Response response;
    try {
      response = await request(await _headers()).timeout(_timeout);
    } on TimeoutException {
      throw const ApiException(408, 'انتهت مهلة الاتصال بالخادم');
    } on http.ClientException catch (e) {
      throw ApiException(0, 'تعذر الاتصال بالخادم: ${e.message}');
    }

    dynamic body;
    try {
      body = response.body.isEmpty ? null : jsonDecode(response.body);
    } catch (_) {
      body = null;
    }
    if (response.statusCode < 200 || response.statusCode >= 300) {
      final message = body is Map<String, dynamic>
          ? (body['detail'] ?? body['title'] ?? body['message'] ?? 'API request failed')
          : 'API request failed';
      throw ApiException(response.statusCode, message.toString());
    }
    return body;
  }

  Future<dynamic> get(String path) => _send((headers) => http.get(Uri.parse('$baseUrl/$path'), headers: headers));
  Future<dynamic> post(String path, Map<String, dynamic> data) => _send((headers) => http.post(Uri.parse('$baseUrl/$path'), headers: headers, body: jsonEncode(data)));
  Future<dynamic> put(String path, Map<String, dynamic> data) => _send((headers) => http.put(Uri.parse('$baseUrl/$path'), headers: headers, body: jsonEncode(data)));
  Future<void> delete(String path) async { await _send((headers) => http.delete(Uri.parse('$baseUrl/$path'), headers: headers)); }
}
