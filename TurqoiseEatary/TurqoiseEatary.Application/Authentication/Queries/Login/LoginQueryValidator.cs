using System.Security.Cryptography.X509Certificates;
using FluentValidation;

namespace TurqoiseEatary.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("bu denemeydi zaaa xd");
        RuleFor(x => x.Password).NotEmpty();
    }

}