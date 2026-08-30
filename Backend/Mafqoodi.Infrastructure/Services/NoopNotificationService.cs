using Mafqoodi.Application.Services;

namespace Mafqoodi.Infrastructure.Services;

public sealed class NoopNotificationService : INotificationService
{
    public Task CreateAsync(Guid userId, string title, string body, string? type, CancellationToken ct)
        => Task.CompletedTask; // يترك الإرسال لمزود Push مستقل
}
