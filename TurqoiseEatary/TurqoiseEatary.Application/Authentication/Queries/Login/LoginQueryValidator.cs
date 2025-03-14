using System.Security.Cryptography.X509Certificates;
using FluentValidation;

namespace TurqoiseEatary.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("example usage with message");
        RuleFor(x => x.Password).NotEmpty();
    }

}