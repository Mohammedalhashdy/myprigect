using System.Security.Cryptography;
using System.Collections.Concurrent;

namespace Mafqoodi.Application.Services;

public interface IOtpService
{
    string Create(Guid userId, TimeSpan lifetime);
    bool Verify(Guid userId, string code);
}

public sealed class OtpService : IOtpService
{
    private readonly ConcurrentDictionary<Guid, (string Hash, DateTime ExpiresAt)> _codes = new();

    public string Create(Guid userId, TimeSpan lifetime)
    {
        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        _codes[userId] = (Convert.ToHexString(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(code))), DateTime.UtcNow.Add(lifetime));
        return code;
    }

    public bool Verify(Guid userId, string code)
    {
        if (!_codes.TryGetValue(userId, out var item) || item.ExpiresAt < DateTime.UtcNow) return false;
        var hash = Convert.ToHexString(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(code)));
        var valid = CryptographicOperations.FixedTimeEquals(Convert.FromHexString(item.Hash), Convert.FromHexString(hash));
        if (valid) _codes.TryRemove(userId, out _); // حذف الرمز بعد الاستخدام
        return valid;
    }
}
