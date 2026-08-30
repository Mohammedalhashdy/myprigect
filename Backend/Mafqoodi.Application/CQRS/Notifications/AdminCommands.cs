using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Application.CQRS.Notifications;

public sealed record BroadcastNotificationCommand(string Title, string Body) : IRequest<int>;

public sealed class BroadcastNotificationHandler(IUserRepository users, INotificationRepository notifications)
    : IRequestHandler<BroadcastNotificationCommand, int>
{
    public async Task<int> Handle(BroadcastNotificationCommand request, CancellationToken ct)
    {
        var title = request.Title.Trim();
        var body = request.Body.Trim();
        if (title.Length == 0 || body.Length == 0) throw new ArgumentException("عنوان ونص الإشعار مطلوبان.");

        var items = (await users.GetAllAsync(ct)).Select(user => new Notification
        {
            Id = Guid.NewGuid(), UserId = user.Id, Title = title, Body = body
        }).ToList();
        await notifications.AddRangeAsync(items, ct);
        await notifications.SaveChangesAsync(ct);
        return items.Count;
    }
}
