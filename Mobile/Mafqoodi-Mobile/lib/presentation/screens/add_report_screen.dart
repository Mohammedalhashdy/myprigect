import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/app_providers.dart';

class AddReportScreen extends ConsumerStatefulWidget {
  const AddReportScreen({super.key});
  @override State<AddReportScreen> createState() => _AddReportScreenState();
}

class _AddReportScreenState extends ConsumerState<AddReportScreen> {
  final title = TextEditingController();
  final description = TextEditingController();
  final location = TextEditingController();
  String type = 'lost';
  bool saving = false;

  @override void dispose(){title.dispose();description.dispose();location.dispose();super.dispose();}

  Future<void> save() async {
    if (title.text.trim().isEmpty || description.text.trim().isEmpty) return;
    setState(() => saving = true);
    try {
      await ref.read(reportRepositoryProvider).createReport({'title': title.text.trim(), 'description': description.text.trim(), 'locationName': location.text.trim(), 'reportType': type});
      ref.invalidate(reportsProvider); ref.invalidate(myReportsProvider);
      if (mounted) ScaffoldMessenger.of(context).showSnackBar(const SnackBar(content: Text('تم إنشاء البلاغ بنجاح')));
      title.clear(); description.clear(); location.clear();
    } catch (e) { if (mounted) ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(e.toString()))); }
    finally { if (mounted) setState(() => saving = false); }
  }

  @override Widget build(BuildContext context) => ListView(padding: const EdgeInsets.all(20), children: [
    Text('إضافة بلاغ', style: Theme.of(context).textTheme.headlineSmall),
    const SizedBox(height: 18),
    DropdownButtonFormField<String>(value: type, decoration: const InputDecoration(labelText: 'نوع البلاغ'), items: const [DropdownMenuItem(value:'lost',child:Text('مفقود')),DropdownMenuItem(value:'found',child:Text('معثور عليه'))], onChanged: (v) => setState(() => type = v ?? 'lost')),
    const SizedBox(height: 12), TextField(controller:title, decoration:const InputDecoration(labelText:'العنوان')), const SizedBox(height:12),
    TextField(controller:description,maxLines:5,decoration:const InputDecoration(labelText:'الوصف')), const SizedBox(height:12),
    TextField(controller:location,decoration:const InputDecoration(labelText:'الموقع')), const SizedBox(height:20),
    FilledButton(onPressed:saving?null:save,child:saving?const CircularProgressIndicator():const Text('نشر البلاغ')),
  ]);
}
