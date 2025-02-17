using System.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TurqoiseEatary.Application.Services.Authentication;
using TurqoiseEatary.Contracts.Authentication;
using IAuthenticationService = TurqoiseEatary.Application.Services.Authentication.IAuthenticationService;
using LoginRequest = TurqoiseEatary.Contracts.Authentication.LoginRequest;
using RegisterRequest = TurqoiseEatary.Contracts.Authentication.RegisterRequest;

namespace TurqoiseEatary.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest login)
    {
        var authResult = _authenticationService.Login(login.Email, login.Password);
        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );

        return Ok(response);
    }


}