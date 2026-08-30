using Mafqoodi.Application.Abstractions;

namespace Mafqoodi.Infrastructure.Services;

public sealed class DisabledGeminiMatchingProvider : IGeminiMatchingProvider
{
    public Task<IReadOnlyDictionary<Guid, int>> ScoreAsync(string sourceTitle, string sourceDescription, IReadOnlyList<(Guid Id, string Title, string Description)> candidates, CancellationToken ct)
        => Task.FromResult<IReadOnlyDictionary<Guid, int>>(new Dictionary<Guid, int>());
}
