using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Application.CQRS.Admin;
using Mafqoodi.Application.CQRS.Notifications;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "admin")]
public sealed class AdminController(IMediator mediator) : ControllerBase
{
    [HttpGet("statistics")]
    public async Task<ActionResult<DashboardStatisticsResponse>> Statistics(CancellationToken ct)
        => Ok(await mediator.Send(new GetDashboardStatisticsQuery(), ct));

    [HttpGet("users")]
    public async Task<ActionResult<IReadOnlyList<UserSummaryResponse>>> Users(CancellationToken ct)
        => Ok(await mediator.Send(new GetUsersQuery(), ct));

    [HttpPatch("users/{id:guid}/status")]
    public async Task<IActionResult> UpdateUserStatus(Guid id, UpdateUserStatusRequest request, CancellationToken ct)
    {
        await mediator.Send(new UpdateUserStatusCommand(id, request.IsBanned), ct);
        return NoContent();
    }

    [HttpPost("users/{id:guid}/assign-admin")]
    public async Task<IActionResult> AssignAdmin(Guid id, CancellationToken ct)
    {
        await mediator.Send(new AssignAdminRoleCommand(id), ct);
        return NoContent();
    }

    [HttpPatch("reports/{id:guid}/status")]
    public async Task<IActionResult> UpdateReportStatus(Guid id, UpdateAdminStatusRequest request, CancellationToken ct)
    {
        await mediator.Send(new UpdateReportAdminStatusCommand(id, request.Status), ct);
        return NoContent();
    }

    [HttpPost("notifications/broadcast")]
    public async Task<ActionResult<object>> Broadcast(BroadcastRequest request, CancellationToken ct)
        => Ok(new { recipients = await mediator.Send(new BroadcastNotificationCommand(request.Title, request.Body), ct) });

    public sealed record BroadcastRequest(string Title, string Body);
}
