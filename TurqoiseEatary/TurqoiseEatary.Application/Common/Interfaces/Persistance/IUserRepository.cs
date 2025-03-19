using TurqoiseEatary.Domain.User;

namespace TurqoiseEatary.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    public User? GetUserByMail(string mail);
    public void Add(User user);

}