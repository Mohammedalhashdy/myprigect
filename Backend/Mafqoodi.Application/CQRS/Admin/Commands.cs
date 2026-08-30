using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.Application.CQRS.Admin;

public sealed record UpdateUserStatusCommand(Guid UserId, bool IsBanned) : IRequest<Unit>;
public sealed record AssignAdminRoleCommand(Guid UserId) : IRequest<Unit>;
public sealed record UpdateReportAdminStatusCommand(Guid ReportId, string Status) : IRequest<Unit>;

public sealed class UpdateUserStatusCommandHandler(IUserRepository users) : IRequestHandler<UpdateUserStatusCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserStatusCommand c, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(c.UserId, ct) ?? throw new KeyNotFoundException("المستخدم غير موجود.");
        user.IsBanned = c.IsBanned;
        await users.SaveChangesAsync(ct);
        return Unit.Value;
    }
}

public sealed class AssignAdminRoleCommandHandler(IUserRepository users) : IRequestHandler<AssignAdminRoleCommand, Unit>
{
    public async Task<Unit> Handle(AssignAdminRoleCommand c, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(c.UserId, ct) ?? throw new KeyNotFoundException("المستخدم غير موجود.");
        user.Role = "admin";
        await users.SaveChangesAsync(ct);
        return Unit.Value;
    }
}

public sealed class UpdateReportAdminStatusCommandHandler(IReportRepository reports) : IRequestHandler<UpdateReportAdminStatusCommand, Unit>
{
    public async Task<Unit> Handle(UpdateReportAdminStatusCommand c, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(c.ReportId, ct) ?? throw new KeyNotFoundException("البلاغ غير موجود.");
        report.AdminStatus = c.Status.Trim().ToLowerInvariant();
        await reports.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
