using TurqoiseEatary.Domain.User;

namespace TurqoiseEatary.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);

}