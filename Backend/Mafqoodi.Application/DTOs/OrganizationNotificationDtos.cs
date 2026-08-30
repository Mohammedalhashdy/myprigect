namespace Mafqoodi.Application.DTOs;

public sealed record OrganizationResponse(Guid Id, string Name, string? Description, string? Phone, string? Address, string? LogoUrl, bool IsActive);
public sealed record NotificationResponse(Guid Id, string Title, string Body, bool IsRead, DateTime CreatedAt);
