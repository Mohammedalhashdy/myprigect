using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.Application.CQRS.Reports;

public sealed record GetReportsQuery(string? Category, string? ReportType, string? Status) : IRequest<IReadOnlyList<ReportResponse>>;
public sealed record GetReportByIdQuery(Guid ReportId) : IRequest<ReportResponse>;
public sealed record GetMyReportsQuery(Guid UserId) : IRequest<IReadOnlyList<ReportResponse>>;

public sealed class GetReportsQueryHandler(IReportRepository reports) : IRequestHandler<GetReportsQuery, IReadOnlyList<ReportResponse>>
{
    public async Task<IReadOnlyList<ReportResponse>> Handle(GetReportsQuery q, CancellationToken ct)
        => (await reports.GetAsync(q.Category, q.ReportType, q.Status, ct)).Select(ReportMapper.ToResponse).ToList();
}

public sealed class GetReportByIdQueryHandler(IReportRepository reports) : IRequestHandler<GetReportByIdQuery, ReportResponse>
{
    public async Task<ReportResponse> Handle(GetReportByIdQuery q, CancellationToken ct)
        => ReportMapper.ToResponse(await reports.GetByIdAsync(q.ReportId, ct) ?? throw new KeyNotFoundException("البلاغ غير موجود."));
}

public sealed class GetMyReportsQueryHandler(IReportRepository reports) : IRequestHandler<GetMyReportsQuery, IReadOnlyList<ReportResponse>>
{
    public async Task<IReadOnlyList<ReportResponse>> Handle(GetMyReportsQuery q, CancellationToken ct)
        => (await reports.GetByUserAsync(q.UserId, ct)).Select(ReportMapper.ToResponse).ToList();
}
