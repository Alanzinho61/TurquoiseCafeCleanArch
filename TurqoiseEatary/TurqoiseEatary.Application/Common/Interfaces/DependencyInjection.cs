using System.Reflection;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TurqoiseEatary.Application.Authentication.Commands.Register;
using TurqoiseEatary.Application.Common.Behaviors;
using TurqoiseEatary.Application.Services.Authentication.Common;

namespace TurqoiseEatary.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}