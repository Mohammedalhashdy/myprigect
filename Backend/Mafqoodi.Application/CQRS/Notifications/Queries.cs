using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.Application.CQRS.Notifications;

public sealed record GetNotificationsQuery(Guid UserId) : IRequest<IReadOnlyList<NotificationResponse>>;
public sealed record MarkNotificationReadCommand(Guid UserId, Guid NotificationId) : IRequest;

public sealed class GetNotificationsHandler(INotificationRepository repository)
    : IRequestHandler<GetNotificationsQuery, IReadOnlyList<NotificationResponse>>
{
    public async Task<IReadOnlyList<NotificationResponse>> Handle(GetNotificationsQuery request, CancellationToken ct)
        => (await repository.GetForUserAsync(request.UserId, ct))
            .Select(x => new NotificationResponse(x.Id, x.Title, x.Body, x.IsRead, x.CreatedAt)).ToList();
}

public sealed class MarkNotificationReadHandler(INotificationRepository repository)
    : IRequestHandler<MarkNotificationReadCommand>
{
    public async Task Handle(MarkNotificationReadCommand request, CancellationToken ct)
    {
        await repository.MarkAsReadAsync(request.UserId, request.NotificationId, ct);
        await repository.SaveChangesAsync(ct);
    }
}
