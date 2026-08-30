namespace Mafqoodi.Application.Services;

public interface INotificationService
{
    Task CreateAsync(Guid userId, string title, string body, string? type, CancellationToken ct);
}
