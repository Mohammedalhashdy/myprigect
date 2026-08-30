import 'package:flutter_test/flutter_test.dart';
import 'package:mafqoodi_mobile/main.dart';

void main() {
  testWidgets('application starts with login screen', (tester) async {
    await tester.pumpWidget(const MafqoodiApp());
    expect(find.text('مفقودي'), findsOneWidget);
    expect(find.text('تسجيل الدخول'), findsOneWidget);
  });
}
