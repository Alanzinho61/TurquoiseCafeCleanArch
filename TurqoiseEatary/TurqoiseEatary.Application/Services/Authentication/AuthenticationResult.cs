using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Services.Authentication;

public record AuthenticationResult(
    User user,
    string Token
);

