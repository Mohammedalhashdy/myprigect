import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../application/cqrs/auth_commands.dart';
import '../providers/app_providers.dart';
import 'main_shell.dart';

class LoginScreen extends ConsumerStatefulWidget {
  const LoginScreen({super.key});
  @override State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends ConsumerState<LoginScreen> {
  final email = TextEditingController();
  final password = TextEditingController();
  bool loading = false;

  @override void dispose() { email.dispose(); password.dispose(); super.dispose(); }

  Future<void> _login() async {
    if (email.text.trim().isEmpty || password.text.isEmpty) return;
    setState(() => loading = true);
    try {
      final session = await LoginHandler(ref.read(authRepositoryProvider)).execute(LoginCommand(email.text, password.text));
      ref.read(authSessionProvider.notifier).state = session;
      if (mounted) Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (_) => const MainShell()));
    } catch (e) {
      if (mounted) ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(e.toString())));
    } finally { if (mounted) setState(() => loading = false); }
  }

  @override
  Widget build(BuildContext context) => Scaffold(
    body: SafeArea(child: Center(child: SingleChildScrollView(padding: const EdgeInsets.all(28), child: ConstrainedBox(constraints: const BoxConstraints(maxWidth: 460), child: Column(crossAxisAlignment: CrossAxisAlignment.stretch, children: [
      const Icon(Icons.search_rounded, size: 78),
      const SizedBox(height: 18),
      Text('مفقودي', textAlign: TextAlign.center, style: Theme.of(context).textTheme.headlineMedium?.copyWith(fontWeight: FontWeight.bold)),
      const SizedBox(height: 32),
      TextField(controller: email, keyboardType: TextInputType.emailAddress, decoration: const InputDecoration(labelText: 'البريد الإلكتروني', prefixIcon: Icon(Icons.email_outlined))),
      const SizedBox(height: 16),
      TextField(controller: password, obscureText: true, decoration: const InputDecoration(labelText: 'كلمة المرور', prefixIcon: Icon(Icons.lock_outline))),
      const SizedBox(height: 24),
      FilledButton(onPressed: loading ? null : _login, child: loading ? const SizedBox(height: 22,width:22,child:CircularProgressIndicator()) : const Text('تسجيل الدخول')),
    ]))))));
}
