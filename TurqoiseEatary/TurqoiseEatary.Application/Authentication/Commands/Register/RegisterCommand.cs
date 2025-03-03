using ErrorOr;
using MediatR;
using TurqoiseEatary.Application.Authentication.Common;

namespace TurqoiseEatary.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;