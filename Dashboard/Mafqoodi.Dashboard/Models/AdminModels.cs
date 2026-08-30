namespace Mafqoodi.Dashboard.Models;

public sealed record AuthViewModel(Guid UserId, string Name, string Email, string Role, string Token);
public sealed record DashboardStatisticsViewModel(int TotalUsers, int TotalReports, int ActiveReports, int ResolvedReports);
public sealed record UserViewModel(Guid Id, string Name, string Email, string? PhoneNumber, string Role, bool IsBanned, DateTime CreatedAt);
public sealed record ReportViewModel(Guid Id, string Title, string Description, string LocationName, string ReportType, string Category, string Status, string AdminStatus, decimal RewardAmount, string RewardCurrency, DateTime CreatedAt);
public sealed record LoginViewModel(string Email, string Password);
