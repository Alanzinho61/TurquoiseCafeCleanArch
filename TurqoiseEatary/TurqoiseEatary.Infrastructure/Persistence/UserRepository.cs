using TurqoiseEatary.Application.Common.Interfaces.Persistance;
using TurqoiseEatary.Domain.Entities;

namespace TurqoiseEatary.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByMail(string mail)
    {
        return _users.SingleOrDefault(u => u.Email == mail);
    }
}