using System.Data;
using ErrorOr;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TurqoiseEatary.Application.Services.Authentication;
using TurqoiseEatary.Contracts.Authentication;
using IAuthenticationService = TurqoiseEatary.Application.Services.Authentication.IAuthenticationService;
using LoginRequest = TurqoiseEatary.Contracts.Authentication.LoginRequest;
using RegisterRequest = TurqoiseEatary.Contracts.Authentication.RegisterRequest;

namespace TurqoiseEatary.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
        // return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest login)
    {
        var authResult = _authenticationService.Login(
            login.Email,
            login.Password);

        return authResult.Match(
        authResult => Ok(MapAuthResult(authResult)),
        errors => Problem(errors));
    }
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.user.Id,
                    authResult.user.FirstName,
                    authResult.user.LastName,
                    authResult.user.Email,
                    authResult.Token
                );
    }


}