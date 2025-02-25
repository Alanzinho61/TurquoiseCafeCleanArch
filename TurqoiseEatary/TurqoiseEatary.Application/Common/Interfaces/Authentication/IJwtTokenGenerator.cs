using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);

}