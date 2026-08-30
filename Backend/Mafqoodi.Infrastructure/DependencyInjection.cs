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
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<ISupportRepository, SupportRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ISmartMatchingService, SmartMatchingService>();
        services.AddSingleton<IOtpService, OtpService>(); // OTP مؤقت وآمن
        return services;
    }
}
