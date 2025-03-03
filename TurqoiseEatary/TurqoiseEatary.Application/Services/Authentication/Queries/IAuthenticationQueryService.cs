using ErrorOr;
using TurqoiseEatary.Application.Services.Authentication.Common;
namespace TurqoiseEatary.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}