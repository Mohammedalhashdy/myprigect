import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import '../../core/network/api_client.dart';

class AuthSession {
  const AuthSession({required this.userId, required this.name, required this.email, required this.role, required this.token});
  final String userId;
  final String name;
  final String email;
  final String role;
  final String token;
}

abstract interface class IAuthRepository {
  Future<AuthSession> login(String email, String password);
  Future<AuthSession> register(Map<String, dynamic> request);
  Future<void> logout();
}

class AuthRepository implements IAuthRepository {
  AuthRepository(this._api, this._storage);
  final ApiClient _api;
  final FlutterSecureStorage _storage;

  AuthSession _map(Map<String, dynamic> data) => AuthSession(
    userId: data['userId'].toString(), name: data['name'] ?? '', email: data['email'] ?? '',
    role: data['role'] ?? 'user', token: data['token'] ?? '');

  @override
  Future<AuthSession> login(String email, String password) async {
    final session = _map(await _api.post('api/auth/login', {'email': email, 'password': password}) as Map<String, dynamic>);
    await _storage.write(key: 'access_token', value: session.token);
    await _storage.write(key: 'user_id', value: session.userId);
    return session;
  }

  @override
  Future<AuthSession> register(Map<String, dynamic> request) async {
    final session = _map(await _api.post('api/auth/register', request) as Map<String, dynamic>);
    await _storage.write(key: 'access_token', value: session.token);
    await _storage.write(key: 'user_id', value: session.userId);
    return session;
  }

  @override
  Future<void> logout() async {
    await _storage.delete(key: 'access_token');
    await _storage.delete(key: 'user_id');
  }
}
