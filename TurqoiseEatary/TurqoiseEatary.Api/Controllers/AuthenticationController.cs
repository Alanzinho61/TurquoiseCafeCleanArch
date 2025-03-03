using System.Data;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TurqoiseEatary.Application.Authentication.Commands.Register;
using TurqoiseEatary.Application.Services.Authentication;
using TurqoiseEatary.Application.Authentication.Common;
using TurqoiseEatary.Contracts.Authentication;
using IAuthenticationCommandService = TurqoiseEatary.Application.Services.Authentication.Command.IAuthenticationCommandService;
using LoginRequest = TurqoiseEatary.Contracts.Authentication.LoginRequest;
using RegisterRequest = TurqoiseEatary.Contracts.Authentication.RegisterRequest;
using TurqoiseEatary.Application.Authentication.Queries.Login;

namespace TurqoiseEatary.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        var query = new LoginQuery(login.Email, login.Password);
        var authResult = await _mediator.Send(query);

        //Buradaki hata kismini duzelt

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