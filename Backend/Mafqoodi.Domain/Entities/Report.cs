namespace Mafqoodi.Domain.Entities;

public sealed class Report
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string? PublisherPhone { get; set; }
    public string PublisherAccountType { get; set; } = "personal";
    public required string ReportType { get; set; }
    public string? Category { get; set; }
    public string? CustomCategoryName { get; set; }
    public decimal? RewardAmount { get; set; }
    public string? RewardCurrency { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "active";
    public string AdminStatus { get; set; } = "normal";
    public string? ImageData { get; set; }
    public string? Base64Image { get; set; }
    public ICollection<ReportFlag> Flags { get; set; } = new List<ReportFlag>();
}

public sealed class ReportFlag
{
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public Report? Report { get; set; }
    public required Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
