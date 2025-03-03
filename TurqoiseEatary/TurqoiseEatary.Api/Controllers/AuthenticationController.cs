using System.Data;
using ErrorOr;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TurqoiseEatary.Application.Services.Authentication;
using TurqoiseEatary.Application.Services.Authentication.Command;
using TurqoiseEatary.Application.Services.Authentication.Common;
using TurqoiseEatary.Application.Services.Authentication.Queries;
using TurqoiseEatary.Contracts.Authentication;
using IAuthenticationCommandService = TurqoiseEatary.Application.Services.Authentication.Command.IAuthenticationCommandService;
using LoginRequest = TurqoiseEatary.Contracts.Authentication.LoginRequest;
using RegisterRequest = TurqoiseEatary.Contracts.Authentication.RegisterRequest;

namespace TurqoiseEatary.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationCommandService _authenticationCommandService;
    private readonly IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(
    IAuthenticationCommandService authenticationCommandService,
    IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(
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
        var authResult = _authenticationQueryService.Login(
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