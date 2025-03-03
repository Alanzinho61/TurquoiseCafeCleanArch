using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User user,
    string Token
);

