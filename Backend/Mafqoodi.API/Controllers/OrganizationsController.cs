using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Application.CQRS.Organizations;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.API.Controllers;

[ApiController]
[Route("api/organizations")]
public sealed class OrganizationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrganizationResponse>>> Get(CancellationToken ct)
        => Ok(await mediator.Send(new GetOrganizationsQuery(), ct));
}
