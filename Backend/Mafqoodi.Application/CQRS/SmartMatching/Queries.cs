using MediatR;
using Mafqoodi.Application.Abstractions;

namespace Mafqoodi.Application.CQRS.SmartMatching;

public sealed record FindSmartMatchesQuery(Guid ReportId, double MaxDistanceKm = 50) : IRequest<IReadOnlyList<SmartMatchResult>>;

public sealed class FindSmartMatchesQueryHandler(IReportRepository reports, ISmartMatchingService matcher)
    : IRequestHandler<FindSmartMatchesQuery, IReadOnlyList<SmartMatchResult>>
{
    public async Task<IReadOnlyList<SmartMatchResult>> Handle(FindSmartMatchesQuery query, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(query.ReportId, ct) ?? throw new KeyNotFoundException("البلاغ غير موجود.");
        return await matcher.FindMatchesAsync(report, query.MaxDistanceKm, ct);
    }
}
