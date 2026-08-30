import '../../data/repositories/auth_repository.dart';

class LoginCommand {
  const LoginCommand(this.email, this.password);
  final String email;
  final String password;
}

class LoginHandler {
  LoginHandler(this.repository);
  final IAuthRepository repository;
  Future<AuthSession> execute(LoginCommand command) => repository.login(command.email.trim(), command.password);
}

class LogoutCommand { const LogoutCommand(); }
class LogoutHandler {
  LogoutHandler(this.repository);
  final IAuthRepository repository;
  Future<void> execute(LogoutCommand command) => repository.logout();
}
