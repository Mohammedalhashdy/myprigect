import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../domain/entities/report.dart';
import '../providers/app_providers.dart';

class ReportsScreen extends ConsumerStatefulWidget {
  const ReportsScreen({super.key});
  @override State<ReportsScreen> createState() => _ReportsScreenState();
}

class _ReportsScreenState extends ConsumerState<ReportsScreen> {
  String? type;
  String? category;

  @override
  Widget build(BuildContext context) {
    final reports = ref.watch(reportsProvider);
    return Column(children: [
      Padding(
        padding: const EdgeInsets.fromLTRB(16, 12, 16, 4),
        child: Row(children: [
          Expanded(child: DropdownButtonFormField<String>(
            value: type,
            decoration: const InputDecoration(labelText: 'نوع البلاغ', prefixIcon: Icon(Icons.filter_alt_outlined)),
            items: const [
              DropdownMenuItem(value: 'lost', child: Text('مفقود')),
              DropdownMenuItem(value: 'found', child: Text('معثور عليه')),
            ],
            onChanged: (v) => setState(() => type = v),
          )),
          const SizedBox(width: 8),
          IconButton(tooltip: 'مسح الفلاتر', onPressed: type == null && category == null ? null : () => setState(() { type = null; category = null; }), icon: const Icon(Icons.clear_all)),
        ]),
      ),
      Expanded(child: RefreshIndicator(
        onRefresh: () async => ref.invalidate(reportsProvider),
        child: reports.when(
          loading: () => const Center(child: CircularProgressIndicator()),
          error: (e, _) => ListView(children: [Padding(padding: const EdgeInsets.all(24), child: Center(child: Text('تعذر تحميل البلاغات\n$e'), textAlign: TextAlign.center))]),
          data: (items) {
            final filtered = type == null ? items : items.where((r) => r.reportType == type).toList();
            if (filtered.isEmpty) return ListView(children: const [Padding(padding: EdgeInsets.all(40), child: Center(child: Text('لا توجد بلاغات مطابقة')))]);
            return ListView.separated(
              padding: const EdgeInsets.all(16),
              itemCount: filtered.length,
              separatorBuilder: (_, __) => const SizedBox(height: 10),
              itemBuilder: (_, i) => _ReportCard(report: filtered[i]),
            );
          },
        ),
      )),
    ]);
  }
}

class _ReportCard extends StatelessWidget {
  const _ReportCard({required this.report});
  final Report report;

  @override
  Widget build(BuildContext context) => Card(
    clipBehavior: Clip.antiAlias,
    child: ListTile(
      contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      leading: CircleAvatar(child: Icon(report.reportType == 'lost' ? Icons.search : Icons.check_circle_outline)),
      title: Text(report.title, maxLines: 1, overflow: TextOverflow.ellipsis),
      subtitle: Text('${report.category ?? 'غير مصنف'} • ${report.locationName}', maxLines: 2, overflow: TextOverflow.ellipsis),
      trailing: Text(report.status),
      onTap: () => Navigator.of(context).push(MaterialPageRoute(builder: (_) => ReportDetailsScreen(report: report))),
    ),
  );
}

class ReportDetailsScreen extends StatelessWidget {
  const ReportDetailsScreen({super.key, required this.report});
  final Report report;

  @override
  Widget build(BuildContext context) => Scaffold(
    appBar: AppBar(title: const Text('تفاصيل البلاغ')),
    body: ListView(padding: const EdgeInsets.all(20), children: [
      Text(report.title, style: Theme.of(context).textTheme.headlineSmall),
      const SizedBox(height: 8),
      Chip(label: Text(report.reportType == 'lost' ? 'مفقود' : 'معثور عليه')),
      const SizedBox(height: 16),
      _Info(title: 'الوصف', value: report.description),
      _Info(title: 'الموقع', value: report.locationName),
      if (report.category != null) _Info(title: 'التصنيف', value: report.customCategoryName ?? report.category!),
      if (report.rewardAmount != null) _Info(title: 'المكافأة', value: '${report.rewardAmount} ${report.rewardCurrency ?? ''}'.trim()),
      _Info(title: 'الحالة', value: report.status),
      if (report.adminStatus != null) _Info(title: 'حالة المراجعة', value: report.adminStatus!),
      if (report.createdAt != null) _Info(title: 'تاريخ النشر', value: report.createdAt!.toLocal().toString()),
      if (report.publisherPhone != null && report.publisherPhone!.isNotEmpty) _Info(title: 'هاتف الناشر', value: report.publisherPhone!),
    ]),
  );
}

class _Info extends StatelessWidget {
  const _Info({required this.title, required this.value});
  final String title;
  final String value;
  @override Widget build(BuildContext context) => Padding(padding: const EdgeInsets.only(bottom: 16), child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [Text(title, style: Theme.of(context).textTheme.labelLarge), const SizedBox(height: 4), Text(value)]));
}
