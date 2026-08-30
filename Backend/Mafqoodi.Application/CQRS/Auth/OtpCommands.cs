using MediatR;
using Mafqoodi.Application.Services;

namespace Mafqoodi.Application.CQRS.Auth;

public sealed record RequestOtpCommand(Guid UserId) : IRequest<string>;
public sealed record VerifyOtpCommand(Guid UserId, string Code) : IRequest<bool>;

public sealed class RequestOtpHandler(IOtpService otp) : IRequestHandler<RequestOtpCommand, string>
{
    public Task<string> Handle(RequestOtpCommand request, CancellationToken ct)
        => Task.FromResult(otp.Create(request.UserId, TimeSpan.FromMinutes(5))); // صلاحية قصيرة
}

public sealed class VerifyOtpHandler(IOtpService otp) : IRequestHandler<VerifyOtpCommand, bool>
{
    public Task<bool> Handle(VerifyOtpCommand request, CancellationToken ct)
        => Task.FromResult(otp.Verify(request.UserId, request.Code));
}
