using TurqoiseEatary.Application.Common.Interfaces;
using TurqoiseEatary.Domain.Menu;

namespace TurqoiseEatary.Infrastructure.Persistence;

public class MenuRepository : IMenuRepository
{
    //public static readonly List<Menu> _menus = new();
    private readonly TurqoiseEataryDbContext _dbContext;

    public MenuRepository(TurqoiseEataryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void add(Menu menu)
    {
        _dbContext.Add(menu);
        _dbContext.SaveChanges();
    }
}
