namespace Mafqoodi.Domain.Entities;

public sealed class SupportChat
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<SupportMessage> Messages { get; set; } = new List<SupportMessage>();
}

public sealed class SupportMessage
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public SupportChat? Chat { get; set; }
    public Guid SenderId { get; set; }
    public required string Body { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
