namespace Mafqoodi.Application.Abstractions;

public interface IGeminiMatchingProvider
{
    Task<IReadOnlyDictionary<Guid, int>> ScoreAsync(
        string sourceTitle,
        string sourceDescription,
        IReadOnlyList<(Guid Id, string Title, string Description)> candidates,
        CancellationToken ct);
}
