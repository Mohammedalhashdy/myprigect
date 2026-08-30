namespace Mafqoodi.Infrastructure.Security;

public sealed class JwtOptions
{
    public string Issuer { get; set; } = "Mafqoodi.Api";
    public string Audience { get; set; } = "Mafqoodi.Client";
    public string Key { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; } = 120;
}
