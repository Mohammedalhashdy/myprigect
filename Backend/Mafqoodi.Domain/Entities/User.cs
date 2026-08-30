namespace Mafqoodi.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Base64Image { get; set; }
    public string AccountType { get; set; } = "personal";
    public string Role { get; set; } = "user";
    public bool IsBanned { get; set; }
    public bool IsPhoneVerified { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Report> Reports { get; set; } = new List<Report>();
}
