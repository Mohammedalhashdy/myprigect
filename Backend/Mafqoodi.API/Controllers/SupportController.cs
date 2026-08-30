using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.API.Extensions;
using Mafqoodi.Application.CQRS.Support;

namespace Mafqoodi.API.Controllers;

[ApiController]
[Route("api/support")]
[Authorize]
public sealed class SupportController(IMediator mediator) : ControllerBase
{
    [HttpPost("chats/{chatId:guid}/messages")]
    public async Task<IActionResult> Send(Guid chatId, [FromBody] SendMessageRequest request, CancellationToken ct)
    {
        var id = await mediator.Send(new SendSupportMessageCommand(User.GetUserId(), chatId, request.Body), ct);
        return Created($"api/support/chats/{chatId}/messages/{id}", new { id });
    }

    public sealed record SendMessageRequest(string Body);
}
