using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.API.Extensions;
using Mafqoodi.Application.CQRS.Reports;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.API.Controllers;

[ApiController]
[Route("api/reports")]
public sealed class ReportsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<ReportResponse>>> Get(string? category, string? reportType, string? status, CancellationToken ct)
        => Ok(await mediator.Send(new GetReportsQuery(category, reportType, status), ct));

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ReportResponse>> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetReportByIdQuery(id), ct));

    [HttpGet("mine")]
    [Authorize]
    public async Task<ActionResult<IReadOnlyList<ReportResponse>>> Mine(CancellationToken ct)
        => Ok(await mediator.Send(new GetMyReportsQuery(User.GetUserId()), ct));

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ReportResponse>> Create(CreateReportRequest request, CancellationToken ct)
        => StatusCode(StatusCodes.Status201Created, await mediator.Send(new CreateReportCommand(User.GetUserId(), request), ct));

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<ReportResponse>> Update(Guid id, UpdateReportRequest request, CancellationToken ct)
        => Ok(await mediator.Send(new UpdateReportCommand(User.GetUserId(), id, request), ct));

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var isAdmin = User.IsInRole("admin");
        await mediator.Send(new DeleteReportCommand(User.GetUserId(), id, isAdmin), ct);
        return NoContent();
    }
}
