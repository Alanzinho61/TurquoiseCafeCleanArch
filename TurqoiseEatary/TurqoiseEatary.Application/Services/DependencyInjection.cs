using Microsoft.Extensions.DependencyInjection;
using TurqoiseEatary.Application.Services.Authentication;

namespace TurqoiseEatary.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;

    }

}