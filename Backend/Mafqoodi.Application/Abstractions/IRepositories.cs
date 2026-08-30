using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Application.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    Task<List<User>> GetAllAsync(CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}

public interface IReportRepository
{
    Task<Report?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Report>> GetAsync(string? category, string? reportType, string? status, CancellationToken ct);
    Task<List<Report>> GetByUserAsync(Guid userId, CancellationToken ct);
    Task AddAsync(Report report, CancellationToken ct);
    void Remove(Report report);
    Task SaveChangesAsync(CancellationToken ct);
}

public interface ISupportRepository
{
    Task<SupportChat?> GetChatAsync(Guid chatId, CancellationToken ct);
    Task<List<SupportChat>> GetChatsAsync(CancellationToken ct);
    Task AddMessageAsync(SupportMessage message, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}

public interface IPasswordService
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}

public interface IJwtService
{
    string CreateToken(User user);
}

public interface ISmartMatchingService
{
    Task<IReadOnlyList<SmartMatchResult>> FindMatchesAsync(Report report, double maxDistanceKm, CancellationToken ct);
}

public sealed record SmartMatchResult(Guid ReportId, int Score, double DistanceKm);
