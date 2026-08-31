using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.Services;
using Mafqoodi.Infrastructure.Persistence;
using Mafqoodi.Infrastructure.Repositories;
using Mafqoodi.Infrastructure.Security;
using Mafqoodi.Infrastructure.Services;

namespace Mafqoodi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.Configure<GeminiOptions>(configuration.GetSection("Gemini"));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<ISupportRepository, SupportRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddHttpClient<HttpGeminiMatchingProvider>((client) => client.Timeout = TimeSpan.FromSeconds(20));
        services.AddScoped<IGeminiMatchingProvider>(sp =>
        {
            var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<GeminiOptions>>().Value;
            return options.Enabled && !string.IsNullOrWhiteSpace(options.ApiKey)
                ? sp.GetRequiredService<HttpGeminiMatchingProvider>()
                : sp.GetRequiredService<DisabledGeminiMatchingProvider>();
        });
        services.AddScoped<ISmartMatchingService, SmartMatchingService>();
        services.AddSingleton<IOtpService, OtpService>(); // OTP مؤقت وآمن
        services.AddScoped<INotificationService, NoopNotificationService>(); // Push يُربط بمزود خارجي
        return services;
    }
}
