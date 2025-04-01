using MediatR;
using TurqoiseEatary.Domain.User;

namespace TurqoiseEatary.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);