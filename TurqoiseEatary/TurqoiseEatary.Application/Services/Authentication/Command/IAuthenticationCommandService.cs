using ErrorOr;
using TurqoiseEatary.Application.Services.Authentication.Common;
namespace TurqoiseEatary.Application.Services.Authentication.Command;

public interface IAuthenticationCommandService
{
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);

}