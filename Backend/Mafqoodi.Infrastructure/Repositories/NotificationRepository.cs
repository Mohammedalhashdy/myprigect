using Microsoft.EntityFrameworkCore;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;
using Mafqoodi.Infrastructure.Persistence;

namespace Mafqoodi.Infrastructure.Repositories;

public sealed class NotificationRepository(ApplicationDbContext db) : INotificationRepository
{
    public Task<List<Notification>> GetForUserAsync(Guid userId, CancellationToken ct)
        => db.Notifications.AsNoTracking().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToListAsync(ct);

    public Task AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken ct)
        => db.Notifications.AddRangeAsync(notifications, ct);

    public async Task MarkAsReadAsync(Guid userId, Guid notificationId, CancellationToken ct)
    {
        var notification = await db.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId && x.UserId == userId, ct);
        if (notification is not null) notification.IsRead = true; // لا يسمح بتعديل إشعار مستخدم آخر
    }

    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}
