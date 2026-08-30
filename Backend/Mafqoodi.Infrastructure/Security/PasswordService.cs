using Microsoft.AspNetCore.Identity;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Infrastructure.Security;

public sealed class PasswordService : IPasswordService
{
    private readonly PasswordHasher<User> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword(new User
    {
        Id = Guid.Empty,
        Name = "hash",
        Email = "hash@local",
        PasswordHash = string.Empty
    }, password);

    public bool Verify(string password, string passwordHash)
        => _hasher.VerifyHashedPassword(new User
        {
            Id = Guid.Empty,
            Name = "verify",
            Email = "verify@local",
            PasswordHash = passwordHash
        }, passwordHash, password) != PasswordVerificationResult.Failed;
}
