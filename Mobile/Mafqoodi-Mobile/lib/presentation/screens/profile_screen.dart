import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/app_providers.dart';

class ProfileScreen extends ConsumerWidget {
  const ProfileScreen({super.key});
  @override Widget build(BuildContext context, WidgetRef ref) {
    final session = ref.watch(authSessionProvider);
    return ListView(padding: const EdgeInsets.all(20), children: [
      const CircleAvatar(radius: 44, child: Icon(Icons.person, size: 42)),
      const SizedBox(height: 16),
      Center(child: Text(session?.name ?? 'مستخدم', style: Theme.of(context).textTheme.titleLarge)),
      Center(child: Text(session?.email ?? '')),
      const SizedBox(height: 24),
      Card(child: ListTile(leading: const Icon(Icons.verified_user_outlined), title: const Text('الدور'), subtitle: Text(session?.role ?? 'user'))),
      Card(child: ListTile(leading: const Icon(Icons.security_outlined), title: const Text('الأمان'), subtitle: const Text('JWT + تخزين آمن للتوكن'))),
    ]);
  }
}
