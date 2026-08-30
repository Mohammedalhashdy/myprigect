using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.Application.CQRS.Admin;

public sealed record GetDashboardStatisticsQuery : IRequest<DashboardStatisticsResponse>;
public sealed record GetUsersQuery : IRequest<IReadOnlyList<UserSummaryResponse>>;

public sealed class GetDashboardStatisticsQueryHandler(IUserRepository users, IReportRepository reports)
    : IRequestHandler<GetDashboardStatisticsQuery, DashboardStatisticsResponse>
{
    public async Task<DashboardStatisticsResponse> Handle(GetDashboardStatisticsQuery q, CancellationToken ct)
    {
        var allUsers = await users.GetAllAsync(ct);
        var allReports = await reports.GetAsync(null, null, null, ct);
        return new DashboardStatisticsResponse(
            allUsers.Count,
            allReports.Count,
            allReports.Count(x => x.Status == "active"),
            allReports.Count(x => x.Status == "resolved"));
    }
}

public sealed class GetUsersQueryHandler(IUserRepository users) : IRequestHandler<GetUsersQuery, IReadOnlyList<UserSummaryResponse>>
{
    public async Task<IReadOnlyList<UserSummaryResponse>> Handle(GetUsersQuery q, CancellationToken ct)
        => (await users.GetAllAsync(ct)).Select(x => new UserSummaryResponse(x.Id, x.Name, x.Email, x.PhoneNumber, x.Role, x.IsBanned, x.CreatedAt)).ToList();
}
