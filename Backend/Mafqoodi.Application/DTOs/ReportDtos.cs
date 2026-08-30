namespace Mafqoodi.Application.DTOs;

public sealed record CreateReportRequest(
    string Title,
    string Description,
    string LocationName,
    double? Latitude,
    double? Longitude,
    string ReportType,
    string? Category,
    string? CustomCategoryName,
    decimal? RewardAmount,
    string? RewardCurrency,
    string? ImageData);

public sealed record UpdateReportRequest(
    string Title,
    string Description,
    string LocationName,
    double? Latitude,
    double? Longitude,
    string ReportType,
    string? Category,
    string? CustomCategoryName,
    decimal? RewardAmount,
    string? RewardCurrency,
    string? ImageData);

public sealed record ReportResponse(
    Guid Id,
    string Title,
    string Description,
    string LocationName,
    double? Latitude,
    double? Longitude,
    Guid UserId,
    string ReportType,
    string? Category,
    string? CustomCategoryName,
    decimal? RewardAmount,
    string? RewardCurrency,
    DateTime CreatedAt,
    string Status,
    string AdminStatus,
    string? ImageData,
    string? PublisherPhone);
