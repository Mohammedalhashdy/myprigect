using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.DTOs;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Application.CQRS.Auth;

public sealed record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponse>;
public sealed record LoginCommand(LoginRequest Request) : IRequest<AuthResponse>;

public sealed class RegisterCommandHandler(IUserRepository users, IPasswordService passwords, IJwtService jwt)
    : IRequestHandler<RegisterCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterCommand command, CancellationToken ct)
    {
        var request = command.Request;
        if (await users.EmailExistsAsync(request.Email, ct))
            throw new InvalidOperationException("البريد الإلكتروني مستخدم مسبقاً.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            PhoneNumber = request.PhoneNumber,
            AccountType = string.IsNullOrWhiteSpace(request.AccountType) ? "personal" : request.AccountType,
            PasswordHash = passwords.Hash(request.Password)
        };

        // حفظ المستخدم بعد التحقق من البريد.
        await users.AddAsync(user, ct);
        await users.SaveChangesAsync(ct);
        return new AuthResponse(user.Id, user.Name, user.Email, user.Role, jwt.CreateToken(user));
    }
}

public sealed class LoginCommandHandler(IUserRepository users, IPasswordService passwords, IJwtService jwt)
    : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await users.GetByEmailAsync(command.Request.Email.Trim().ToLowerInvariant(), ct)
            ?? throw new UnauthorizedAccessException("بيانات الدخول غير صحيحة.");

        if (user.IsBanned || !passwords.Verify(command.Request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("بيانات الدخول غير صحيحة.");

        // إنشاء التوكن بعد نجاح المصادقة.
        return new AuthResponse(user.Id, user.Name, user.Email, user.Role, jwt.CreateToken(user));
    }
}
