import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../application/cqrs/auth_commands.dart';
import '../providers/app_providers.dart';
import 'reports_screen.dart';
import 'my_reports_screen.dart';
import 'add_report_screen.dart';
import 'profile_screen.dart';

class MainShell extends ConsumerStatefulWidget {
  const MainShell({super.key});
  @override State<MainShell> createState() => _MainShellState();
}

class _MainShellState extends ConsumerState<MainShell> {
  int index = 0;
  final pages = const [ReportsScreen(), MyReportsScreen(), AddReportScreen(), ProfileScreen(), SettingsPanel()];

  @override
  Widget build(BuildContext context) => Scaffold(
    appBar: AppBar(title: const Text('مفقودي'), centerTitle: true),
    drawer: Drawer(child: SafeArea(child: ListView(children: [
      const UserAccountsDrawerHeader(accountName: Text('مفقودي'), accountEmail: Text('منصة المفقودات والمعثورات')),
      ListTile(leading: const Icon(Icons.home_outlined), title: const Text('الرئيسية'), onTap: () => setState(() { index = 0; Navigator.pop(context); })),
      ListTile(leading: const Icon(Icons.report_outlined), title: const Text('بلاغاتي'), onTap: () => setState(() { index = 1; Navigator.pop(context); })),
      ListTile(leading: const Icon(Icons.support_agent_outlined), title: const Text('الدعم'), onTap: () => showModalBottomSheet(context: context, showDragHandle: true, builder: (_) => const Padding(padding: EdgeInsets.all(24), child: Text('سيتم ربط مركز الدعم عبر API في المرحلة التالية.')))),
      ListTile(leading: const Icon(Icons.settings_outlined), title: const Text('الإعدادات'), onTap: () => setState(() { index = 4; Navigator.pop(context); })),
      ListTile(leading: const Icon(Icons.logout), title: const Text('تسجيل الخروج'), onTap: () async { await LogoutHandler(ref.read(authRepositoryProvider)).execute(const LogoutCommand()); ref.read(authSessionProvider.notifier).state = null; if (context.mounted) Navigator.pop(context); }),
    ]))),
    body: IndexedStack(index: index, children: pages),
    bottomNavigationBar: NavigationBar(selectedIndex: index, onDestinationSelected: (value) => setState(() => index = value), destinations: const [
      NavigationDestination(icon: Icon(Icons.home_outlined), selectedIcon: Icon(Icons.home), label: 'الرئيسية'),
      NavigationDestination(icon: Icon(Icons.list_alt_outlined), selectedIcon: Icon(Icons.list_alt), label: 'بلاغاتي'),
      NavigationDestination(icon: Icon(Icons.add_circle_outline), selectedIcon: Icon(Icons.add_circle), label: 'إضافة'),
      NavigationDestination(icon: Icon(Icons.person_outline), selectedIcon: Icon(Icons.person), label: 'حسابي'),
      NavigationDestination(icon: Icon(Icons.settings_outlined), selectedIcon: Icon(Icons.settings), label: 'الإعدادات'),
    ]),
  );
}

class SettingsPanel extends StatelessWidget {
  const SettingsPanel({super.key});
  @override Widget build(BuildContext context) => ListView(padding: const EdgeInsets.all(20), children: const [
    Card(child: ListTile(leading: Icon(Icons.language), title: Text('اللغة'), subtitle: Text('العربية RTL'))),
    Card(child: ListTile(leading: Icon(Icons.dark_mode_outlined), title: Text('المظهر'), subtitle: Text('Material 3'))),
    Card(child: ListTile(leading: Icon(Icons.info_outline), title: Text('حول التطبيق'), subtitle: Text('Mafqoodi'))),
  ]);
}
