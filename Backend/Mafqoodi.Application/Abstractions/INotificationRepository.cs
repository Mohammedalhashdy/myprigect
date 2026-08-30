using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Application.Abstractions;

public interface INotificationRepository
{
    Task<List<Notification>> GetForUserAsync(Guid userId, CancellationToken ct);
    Task AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken ct);
    Task MarkAsReadAsync(Guid userId, Guid notificationId, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
