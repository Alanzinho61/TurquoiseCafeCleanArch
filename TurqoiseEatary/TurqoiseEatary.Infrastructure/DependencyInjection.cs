using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}