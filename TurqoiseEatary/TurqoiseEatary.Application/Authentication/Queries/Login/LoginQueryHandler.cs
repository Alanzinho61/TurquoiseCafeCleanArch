using ErrorOr;
using MediatR;
using TurqoiseEatary.Application.Common.Interfaces.Authentication;
using TurqoiseEatary.Application.Common.Interfaces.Persistance;
//using TurqoiseEatary.Application.Services.Authentication.Common;
using TurqoiseEatary.Application.Authentication.Common;
using TurqoiseEatary.Application.Authentication.Queries.Login;
using TurqoiseEatary.Domain.Common.Errors;
using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Authentication.Queries.Login;

public class LoginQueryHandler :
IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByMail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;

        }
        if (user.Password != query.Password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };

        }
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}
