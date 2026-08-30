using Microsoft.EntityFrameworkCore;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;
using Mafqoodi.Infrastructure.Persistence;

namespace Mafqoodi.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext db) : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct) => db.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
    public Task<User?> GetByEmailAsync(string email, CancellationToken ct) => db.Users.FirstOrDefaultAsync(x => x.Email == email, ct);
    public Task<bool> EmailExistsAsync(string email, CancellationToken ct) => db.Users.AnyAsync(x => x.Email == email, ct);
    public Task AddAsync(User user, CancellationToken ct) => db.Users.AddAsync(user, ct).AsTask();
    public Task<List<User>> GetAllAsync(CancellationToken ct) => db.Users.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync(ct);
    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}

public sealed class ReportRepository(ApplicationDbContext db) : IReportRepository
{
    public Task<Report?> GetByIdAsync(Guid id, CancellationToken ct) => db.Reports.FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<List<Report>> GetAsync(string? category, string? reportType, string? status, CancellationToken ct)
    {
        var q = db.Reports.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(category)) q = q.Where(x => x.Category == category);
        if (!string.IsNullOrWhiteSpace(reportType)) q = q.Where(x => x.ReportType == reportType);
        if (!string.IsNullOrWhiteSpace(status)) q = q.Where(x => x.Status == status);
        return q.OrderByDescending(x => x.CreatedAt).ToListAsync(ct);
    }

    public Task<List<Report>> GetByUserAsync(Guid userId, CancellationToken ct)
        => db.Reports.AsNoTracking().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToListAsync(ct);

    public Task AddAsync(Report report, CancellationToken ct) => db.Reports.AddAsync(report, ct).AsTask();
    public void Remove(Report report) => db.Reports.Remove(report);
    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}

public sealed class SupportRepository(ApplicationDbContext db) : ISupportRepository
{
    public Task<SupportChat?> GetChatAsync(Guid chatId, CancellationToken ct)
        => db.SupportChats.Include(x => x.Messages.OrderBy(x => x.CreatedAt)).FirstOrDefaultAsync(x => x.Id == chatId, ct);

    public Task<List<SupportChat>> GetChatsAsync(CancellationToken ct)
        => db.SupportChats.AsNoTracking().Include(x => x.Messages.OrderByDescending(m => m.CreatedAt).Take(1)).OrderByDescending(x => x.CreatedAt).ToListAsync(ct);

    public Task AddMessageAsync(SupportMessage message, CancellationToken ct) => db.SupportMessages.AddAsync(message, ct).AsTask();
    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}
