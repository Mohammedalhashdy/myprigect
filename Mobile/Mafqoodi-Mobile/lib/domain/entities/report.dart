class Report {
  const Report({required this.id, required this.title, required this.description, required this.locationName, required this.reportType, this.latitude, this.longitude, this.category, this.rewardAmount, this.rewardCurrency, required this.status});
  final String id;
  final String title;
  final String description;
  final String locationName;
  final String reportType;
  final double? latitude;
  final double? longitude;
  final String? category;
  final double? rewardAmount;
  final String? rewardCurrency;
  final String status;

  factory Report.fromJson(Map<String, dynamic> json) => Report(
    id: json['id'].toString(), title: json['title'] ?? '', description: json['description'] ?? '',
    locationName: json['locationName'] ?? '', reportType: json['reportType'] ?? 'lost',
    latitude: (json['latitude'] as num?)?.toDouble(), longitude: (json['longitude'] as num?)?.toDouble(),
    category: json['category'], rewardAmount: (json['rewardAmount'] as num?)?.toDouble(),
    rewardCurrency: json['rewardCurrency'], status: json['status'] ?? 'active');
}
