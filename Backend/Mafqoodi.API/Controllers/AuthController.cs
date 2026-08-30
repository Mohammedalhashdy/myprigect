using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.Application.CQRS.Auth;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request, CancellationToken ct)
        => StatusCode(StatusCodes.Status201Created, await mediator.Send(new RegisterCommand(request), ct));

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request, CancellationToken ct)
        => Ok(await mediator.Send(new LoginCommand(request), ct));
}
