using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Mafqoodi.Application.Abstractions;

namespace Mafqoodi.Infrastructure.Services;

public sealed class HttpGeminiMatchingProvider(HttpClient httpClient, IOptions<GeminiOptions> options) : IGeminiMatchingProvider
{
    private readonly GeminiOptions _options = options.Value;

    public async Task<IReadOnlyDictionary<Guid, int>> ScoreAsync(
        string sourceTitle,
        string sourceDescription,
        IReadOnlyList<(Guid Id, string Title, string Description)> candidates,
        CancellationToken ct)
    {
        if (!_options.Enabled || string.IsNullOrWhiteSpace(_options.ApiKey) || candidates.Count == 0)
            return new Dictionary<Guid, int>();

        var prompt = $"""
Score how well each candidate matches this lost/found report. Return ONLY a JSON array of objects with id and score (0-100).
Source title: {sourceTitle}
Source description: {sourceDescription}
Candidates:
{string.Join("\n", candidates.Select(c => $"id={c.Id}; title={c.Title}; description={c.Description}"))}
""";

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{Uri.EscapeDataString(_options.Model)}:generateContent?key={Uri.EscapeDataString(_options.ApiKey)}";
        using var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = JsonContent.Create(new
            {
                contents = new[] { new { parts = new[] { new { text = prompt } } } },
                generationConfig = new { temperature = 0, responseMimeType = "application/json" }
            })
        };

        using var response = await httpClient.SendAsync(request, ct);
        if (!response.IsSuccessStatusCode) return new Dictionary<Guid, int>();
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync(ct));
        var text = document.RootElement.GetProperty("candidates")[0]
            .GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
        if (string.IsNullOrWhiteSpace(text)) return new Dictionary<Guid, int>();

        var result = new Dictionary<Guid, int>();
        foreach (var item in JsonSerializer.Deserialize<JsonElement[]>(text) ?? [])
        {
            if (item.TryGetProperty("id", out var id) && item.TryGetProperty("score", out var score)
                && Guid.TryParse(id.GetString(), out var guid))
                result[guid] = Math.Clamp(score.GetInt32(), 0, 100);
        }
        return result;
    }
}
