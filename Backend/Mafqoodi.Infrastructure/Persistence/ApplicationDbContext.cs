using Microsoft.EntityFrameworkCore;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<ReportFlag> ReportFlags => Set<ReportFlag>();
    public DbSet<SupportChat> SupportChats => Set<SupportChat>();
    public DbSet<SupportMessage> SupportMessages => Set<SupportMessage>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(120).IsRequired();
            e.Property(x => x.Email).HasMaxLength(256).IsRequired();
            e.Property(x => x.PhoneNumber).HasMaxLength(30);
            e.Property(x => x.Role).HasMaxLength(30).IsRequired();
            e.Property(x => x.AccountType).HasMaxLength(30).IsRequired();
            e.Property(x => x.PasswordHash).HasMaxLength(512).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Report>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.Description).HasMaxLength(4000).IsRequired();
            e.Property(x => x.LocationName).HasMaxLength(300);
            e.Property(x => x.ReportType).HasMaxLength(30).IsRequired();
            e.Property(x => x.Category).HasMaxLength(100);
            e.Property(x => x.RewardAmount).HasPrecision(18, 2);
            e.Property(x => x.RewardCurrency).HasMaxLength(10);
            e.Property(x => x.Status).HasMaxLength(30).IsRequired();
            e.Property(x => x.AdminStatus).HasMaxLength(30).IsRequired();
            e.HasIndex(x => new { x.ReportType, x.Category, x.Status });
            e.HasIndex(x => new { x.Latitude, x.Longitude });
            e.HasIndex(x => x.CreatedAt);
            e.HasOne(x => x.User).WithMany(x => x.Reports).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ReportFlag>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.ReportId, x.UserId }).IsUnique();
            e.HasOne(x => x.Report).WithMany(x => x.Flags).HasForeignKey(x => x.ReportId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SupportChat>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.UserId).IsUnique();
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SupportMessage>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Body).HasMaxLength(5000).IsRequired();
            e.HasIndex(x => new { x.ChatId, x.CreatedAt });
            e.HasOne(x => x.Chat).WithMany(x => x.Messages).HasForeignKey(x => x.ChatId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Organization>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.HasIndex(x => x.Name);
        });

        modelBuilder.Entity<Notification>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.Body).HasMaxLength(4000).IsRequired();
            e.HasIndex(x => new { x.UserId, x.IsRead, x.CreatedAt });
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
