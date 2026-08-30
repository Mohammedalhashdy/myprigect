namespace Mafqoodi.Application.DTOs;

public sealed record UserSummaryResponse(Guid Id, string Name, string Email, string? PhoneNumber, string Role, bool IsBanned, DateTime CreatedAt);
public sealed record DashboardStatisticsResponse(int TotalUsers, int TotalReports, int ActiveReports, int ResolvedReports);
public sealed record UpdateUserStatusRequest(bool IsBanned);
public sealed record UpdateAdminStatusRequest(string Status);
