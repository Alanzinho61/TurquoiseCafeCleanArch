using ErrorOr;
using TurqoiseEatary.Application.Common.Interfaces.Authentication;
using TurqoiseEatary.Application.Common.Interfaces.Persistance;
using TurqoiseEatary.Domain.Common.Errors;
using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // Check if user exists
        if (_userRepository.GetUserByMail(email) != null)
        {
            return Errors.User.DuplicateEmail;
        }


        // Create user (with guid)

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);

        // Create JWT Token

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Check if user exists
        if (_userRepository.GetUserByMail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
            // throw new Exception("User not exist.");
        }
        // Validate the password is correct
        if (user.Password != password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
            // throw new Exception("Invalid Password");
        }

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}

