using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TurqoiseEatary.Application.Common.Interfaces;
using TurqoiseEatary.Application.Common.Interfaces.Authentication;
using TurqoiseEatary.Application.Common.Interfaces.Persistance;
using TurqoiseEatary.Application.Common.Interfaces.Services;
using TurqoiseEatary.Infrastructure.Authentication;
using TurqoiseEatary.Infrastructure.Persistence;
using TurqoiseEatary.Infrastructure.Services;

namespace TurqoiseEatary.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
        .AddAuth(configuration)
        .AddPersistance();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
    public static IServiceCollection AddPersistance(
        this IServiceCollection services
    )
    {
        services.AddDbContext<TurqoiseEataryDbContext>(options => options.UseSqlServer(
            "Server=DESKTOP-CF4C8LU\\SQLEXPRESS;Database=TurqoiseEataryDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
        )); // Write ConnectionString
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var JwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, JwtSettings);

        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        if (jwtSettings == null)
        {
            throw new Exception("Jwt Settings could not be loaded");
        }

        services.AddSingleton(Options.Create(JwtSettings));
        // services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtSettings.Issuer,
            ValidAudience = JwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtSettings.Secret)
            )
        });
        return services;
    }
}