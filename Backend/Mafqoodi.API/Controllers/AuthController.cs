using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mafqoodi.API.Extensions;
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

    [Authorize]
    [HttpPost("request-otp")]
    public async Task<ActionResult<object>> RequestOtp(CancellationToken ct)
        => Ok(new { code = await mediator.Send(new RequestOtpCommand(User.GetUserId()), ct) });

    [Authorize]
    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] string code, CancellationToken ct)
    {
        var valid = await mediator.Send(new VerifyOtpCommand(User.GetUserId(), code), ct);
        return valid ? Ok(new { verified = true }) : BadRequest(new { verified = false, message = "OTP غير صالح أو منتهي." });
    }
}
