using ErrorOr;
using MediatR;
using TurqoiseEatary.Application.Authentication.Common;

namespace TurqoiseEatary.Application.Authentication.Queries.Login;
public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;