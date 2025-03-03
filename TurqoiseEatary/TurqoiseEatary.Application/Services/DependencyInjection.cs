using Microsoft.Extensions.DependencyInjection;
using TurqoiseEatary.Application.Services.Authentication;
using TurqoiseEatary.Application.Services.Authentication.Command;
using TurqoiseEatary.Application.Services.Authentication.Queries;

namespace TurqoiseEatary.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        return services;

    }

}