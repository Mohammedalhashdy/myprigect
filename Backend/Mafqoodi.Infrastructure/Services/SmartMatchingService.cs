using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Infrastructure.Services;

public sealed class SmartMatchingService(IReportRepository reports) : ISmartMatchingService
{
    public async Task<IReadOnlyList<SmartMatchResult>> FindMatchesAsync(Report report, double maxDistanceKm, CancellationToken ct)
    {
        if (!report.Latitude.HasValue || !report.Longitude.HasValue) return [];
        var opposite = report.ReportType.Equals("lost", StringComparison.OrdinalIgnoreCase) ? "found" : "lost";
        var candidates = await reports.GetAsync(report.Category, opposite, "active", ct);
        return candidates
            .Where(x => x.Id != report.Id && x.Latitude.HasValue && x.Longitude.HasValue)
            .Select(x => (item: x, distance: DistanceKm(report.Latitude.Value, report.Longitude.Value, x.Latitude!.Value, x.Longitude!.Value)))
            .Where(x => x.distance <= maxDistanceKm)
            .Select(x => new SmartMatchResult(x.item.Id, SemanticScore(report, x.item), x.distance))
            .Where(x => x.Score >= 50)
            .OrderByDescending(x => x.Score).ThenBy(x => x.DistanceKm).Take(20).ToList();
    }

    private static int SemanticScore(Report a, Report b)
    {
        var left = Tokenize($"{a.Title} {a.Description}");
        var right = Tokenize($"{b.Title} {b.Description}");
        if (left.Count == 0 || right.Count == 0) return 0;
        var overlap = left.Intersect(right, StringComparer.OrdinalIgnoreCase).Count();
        return Math.Min(100, (int)Math.Round(overlap * 100d / Math.Max(left.Count, right.Count)));
    }

    private static HashSet<string> Tokenize(string value) => value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet(StringComparer.OrdinalIgnoreCase);

    private static double DistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        const double earthRadiusKm = 6371.0088;
        static double Rad(double d) => d * Math.PI / 180d;
        var dLat = Rad(lat2 - lat1); var dLon = Rad(lon2 - lon1);
        var a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(Rad(lat1)) * Math.Cos(Rad(lat2)) * Math.Pow(Math.Sin(dLon / 2), 2);
        return earthRadiusKm * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    }
}
