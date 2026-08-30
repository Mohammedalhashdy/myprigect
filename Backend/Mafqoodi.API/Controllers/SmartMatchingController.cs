using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Application.CQRS.SmartMatching;

namespace Mafqoodi.API.Controllers;

[ApiController]
[Route("api/smart-matching")]
[Authorize]
public sealed class SmartMatchingController(IMediator mediator) : ControllerBase
{
    [HttpGet("{reportId:guid}")]
    public async Task<IActionResult> Find(Guid reportId, [FromQuery] double maxDistanceKm = 50, CancellationToken ct = default)
        => Ok(await mediator.Send(new FindSmartMatchesQuery(reportId, Math.Clamp(maxDistanceKm, 1, 200)), ct));
}
