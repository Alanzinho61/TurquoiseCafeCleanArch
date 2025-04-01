using TurqoiseEatary.Domain.Menu;

namespace TurqoiseEatary.Application.Common.Interfaces;

public interface IMenuRepository
{
    void add(Menu menu);
}