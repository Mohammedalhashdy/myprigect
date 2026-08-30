using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.API.Extensions;
using Mafqoodi.Application.CQRS.Notifications;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.API.Controllers;

[ApiController]
[Authorize]
[Route("api/notifications")]
public sealed class NotificationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<NotificationResponse>>> Get(CancellationToken ct)
        => Ok(await mediator.Send(new GetNotificationsQuery(User.GetUserId()), ct));

    [HttpPatch("{id:guid}/read")]
    public async Task<IActionResult> MarkRead(Guid id, CancellationToken ct)
    {
        await mediator.Send(new MarkNotificationReadCommand(User.GetUserId(), id), ct);
        return NoContent();
    }
}
