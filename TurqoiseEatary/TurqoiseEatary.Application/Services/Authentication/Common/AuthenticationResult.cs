using TurqoiseEatary.Domain.User;

namespace TurqoiseEatary.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User user,
    string Token
);

