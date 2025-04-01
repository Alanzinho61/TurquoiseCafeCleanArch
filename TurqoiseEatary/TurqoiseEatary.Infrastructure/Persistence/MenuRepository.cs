using TurqoiseEatary.Application.Common.Interfaces;
using TurqoiseEatary.Domain.Menu;

namespace TurqoiseEatary.Infrastructure.Persistence;

public class MenuRepository : IMenuRepository
{
    public static readonly List<Menu> _menus = new();

    public void add(Menu menu)
    {
        _menus.Add(menu);
    }
}
