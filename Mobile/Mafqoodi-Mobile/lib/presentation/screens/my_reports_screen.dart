import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/app_providers.dart';

class MyReportsScreen extends ConsumerWidget {
  const MyReportsScreen({super.key});
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final state = ref.watch(myReportsProvider);
    return state.when(
      loading: () => const Center(child: CircularProgressIndicator()),
      error: (e, _) => Center(child: Text('تعذر تحميل بلاغاتك: $e')),
      data: (items) => ListView.builder(padding: const EdgeInsets.all(16), itemCount: items.length, itemBuilder: (_, i) => Card(child: ListTile(title: Text(items[i].title), subtitle: Text(items[i].locationName), trailing: Text(items[i].status)))),
    );
  }
}
