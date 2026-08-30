import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/app_providers.dart';

class ReportsScreen extends ConsumerWidget {
  const ReportsScreen({super.key});
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final reports = ref.watch(reportsProvider);
    return RefreshIndicator(onRefresh: () => ref.refresh(reportsProvider.future), child: reports.when(
      loading: () => const Center(child: CircularProgressIndicator()),
      error: (e, _) => ListView(children: [Padding(padding: const EdgeInsets.all(24), child: Text('تعذر تحميل البلاغات: $e'))]),
      data: (items) => items.isEmpty ? ListView(children: const [Padding(padding: EdgeInsets.all(40), child: Center(child: Text('لا توجد بلاغات حالياً')))]) : ListView.separated(
        padding: const EdgeInsets.all(16), itemCount: items.length, separatorBuilder: (_,__) => const SizedBox(height: 10),
        itemBuilder: (_, i) { final r = items[i]; return Card(child: ListTile(leading: CircleAvatar(child: Icon(r.reportType == 'lost' ? Icons.search : Icons.check)), title: Text(r.title), subtitle: Text('${r.category ?? 'غير مصنف'} • ${r.locationName}'), trailing: Text(r.status))); },
      ),
    ));
  }
}
