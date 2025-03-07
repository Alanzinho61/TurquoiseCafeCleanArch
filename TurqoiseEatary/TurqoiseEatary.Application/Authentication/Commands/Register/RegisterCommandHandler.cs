using ErrorOr;
using MediatR;
using TurqoiseEatary.Application.Common.Interfaces.Authentication;
using TurqoiseEatary.Application.Common.Interfaces.Persistance;
using TurqoiseEatary.Application.Authentication.Common;
using TurqoiseEatary.Domain.Common.Errors;
using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Check if user exists
        if (_userRepository.GetUserByMail(command.Email) != null)
        {
            return Errors.User.DuplicateEmail;
        }


        // Create user (with guid)

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };
        _userRepository.Add(user);

        // Create JWT Token

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);

    }
}