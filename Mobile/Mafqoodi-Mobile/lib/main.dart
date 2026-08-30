import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'presentation/screens/login_screen.dart';
import 'presentation/screens/main_shell.dart';
import 'presentation/providers/app_providers.dart';

void main() => runApp(const ProviderScope(child: MafqoodiApp()));

class MafqoodiApp extends ConsumerWidget {
  const MafqoodiApp({super.key});
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final theme = ThemeData(useMaterial3: true, colorSchemeSeed: const Color(0xFF2563EB), fontFamily: 'Cairo');
    final session = ref.watch(authSessionProvider);
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'مفقودي',
      theme: theme,
      home: session == null ? const LoginScreen() : const MainShell(),
    );
  }
}
