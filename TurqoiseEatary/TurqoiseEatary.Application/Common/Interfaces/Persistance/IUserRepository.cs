using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    public User? GetUserByMail(string mail);
    public void Add(User user);

}