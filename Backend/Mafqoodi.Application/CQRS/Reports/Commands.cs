using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.DTOs;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Application.CQRS.Reports;

public sealed record CreateReportCommand(Guid UserId, CreateReportRequest Request) : IRequest<ReportResponse>;
public sealed record UpdateReportCommand(Guid UserId, Guid ReportId, UpdateReportRequest Request) : IRequest<ReportResponse>;
public sealed record DeleteReportCommand(Guid UserId, Guid ReportId, bool IsAdmin) : IRequest<Unit>;

internal static class ReportMapper
{
    public static ReportResponse ToResponse(Report r) => new(
        r.Id, r.Title, r.Description, r.LocationName, r.Latitude, r.Longitude,
        r.UserId, r.ReportType, r.Category, r.CustomCategoryName, r.RewardAmount,
        r.RewardCurrency, r.CreatedAt, r.Status, r.AdminStatus, r.ImageData, r.PublisherPhone);
}

public sealed class CreateReportCommandHandler(IReportRepository reports, IUserRepository users)
    : IRequestHandler<CreateReportCommand, ReportResponse>
{
    public async Task<ReportResponse> Handle(CreateReportCommand command, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(command.UserId, ct)
            ?? throw new UnauthorizedAccessException("المستخدم غير موجود.");
        var x = command.Request;
        var report = new Report
        {
            Id = Guid.NewGuid(), Title = x.Title.Trim(), Description = x.Description.Trim(),
            LocationName = x.LocationName.Trim(), Latitude = x.Latitude, Longitude = x.Longitude,
            UserId = user.Id, PublisherPhone = user.PhoneNumber, PublisherAccountType = user.AccountType,
            ReportType = x.ReportType.Trim().ToLowerInvariant(), Category = x.Category,
            CustomCategoryName = x.CustomCategoryName, RewardAmount = x.RewardAmount,
            RewardCurrency = x.RewardCurrency, ImageData = x.ImageData
        };
        await reports.AddAsync(report, ct);
        await reports.SaveChangesAsync(ct);
        return ReportMapper.ToResponse(report);
    }
}

public sealed class UpdateReportCommandHandler(IReportRepository reports)
    : IRequestHandler<UpdateReportCommand, ReportResponse>
{
    public async Task<ReportResponse> Handle(UpdateReportCommand command, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(command.ReportId, ct)
            ?? throw new KeyNotFoundException("البلاغ غير موجود.");
        if (report.UserId != command.UserId)
            throw new UnauthorizedAccessException("لا تملك صلاحية تعديل هذا البلاغ.");

        var x = command.Request;
        report.Title = x.Title.Trim(); report.Description = x.Description.Trim();
        report.LocationName = x.LocationName.Trim(); report.Latitude = x.Latitude; report.Longitude = x.Longitude;
        report.ReportType = x.ReportType.Trim().ToLowerInvariant(); report.Category = x.Category;
        report.CustomCategoryName = x.CustomCategoryName; report.RewardAmount = x.RewardAmount;
        report.RewardCurrency = x.RewardCurrency; report.ImageData = x.ImageData;
        await reports.SaveChangesAsync(ct);
        return ReportMapper.ToResponse(report);
    }
}

public sealed class DeleteReportCommandHandler(IReportRepository reports)
    : IRequestHandler<DeleteReportCommand, Unit>
{
    public async Task<Unit> Handle(DeleteReportCommand command, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(command.ReportId, ct)
            ?? throw new KeyNotFoundException("البلاغ غير موجود.");
        if (!command.IsAdmin && report.UserId != command.UserId)
            throw new UnauthorizedAccessException("لا تملك صلاحية حذف هذا البلاغ.");
        reports.Remove(report);
        await reports.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
