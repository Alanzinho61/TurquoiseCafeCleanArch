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
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace TurqoiseEatary.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthenticationController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var command = _mapper.Map<RegisterCommand>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        //var query = new LoginQuery(login.Email, login.Password);
        var query = _mapper.Map<LoginQuery>(login);
        var authResult = await _mediator.Send(query);

        //Buradaki hata kismini duzelt

        return authResult.Match(
        authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
        errors => Problem(errors));
    }

    [HttpGet("test-auth")]
    public IActionResult TestAuth()
    {
        var user = HttpContext.User;
        if (user.Identity.IsAuthenticated)
        {
            return Ok(new { Message = "Token başarılı çalışıyor!", User = user.Identity.Name });
        }
        return Unauthorized();
    }


}