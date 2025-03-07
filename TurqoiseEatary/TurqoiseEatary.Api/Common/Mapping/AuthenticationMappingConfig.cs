using Mapster;
using TurqoiseEatary.Application.Authentication.Commands.Register;
using TurqoiseEatary.Application.Authentication.Common;
using TurqoiseEatary.Application.Authentication.Queries.Login;
using TurqoiseEatary.Contracts.Authentication;

namespace TurqoiseEatary.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest.Token, src => src.Token)
        .Map(dest => dest, src => src.user);
    }

}