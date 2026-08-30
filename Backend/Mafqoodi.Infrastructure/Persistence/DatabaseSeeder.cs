using Microsoft.EntityFrameworkCore;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services, CancellationToken ct = default)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync(ct);
        var email = Environment.GetEnvironmentVariable("MAFQOODI_ADMIN_EMAIL");
        var password = Environment.GetEnvironmentVariable("MAFQOODI_ADMIN_PASSWORD");
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) return;
        if (await db.Users.AnyAsync(x => x.Email == email.ToLowerInvariant(), ct)) return;

        var passwords = services.GetRequiredService<IPasswordService>();
        db.Users.Add(new User
        {
            Id = Guid.NewGuid(), Name = "System Admin", Email = email.Trim().ToLowerInvariant(),
            Role = "admin", AccountType = "personal", PasswordHash = passwords.Hash(password)
        });
        await db.SaveChangesAsync(ct);
    }
}
