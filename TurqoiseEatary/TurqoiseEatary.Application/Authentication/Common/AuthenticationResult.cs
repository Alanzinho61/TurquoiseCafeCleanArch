using MediatR;
using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Authentication.Common;

public record AuthenticationResult(User user, string Token);