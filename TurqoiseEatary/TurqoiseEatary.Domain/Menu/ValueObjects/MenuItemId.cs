using TurqoiseEatary.Domain.Common.Models;
using TurqoiseEatary.Domain.Menu.Entities;

namespace TurqoiseEatary.Domain.Common.ValueObjects;
public sealed class MenuItemId : ValueObject
{
    public Guid Value { get; }
    private MenuItemId(Guid value)
    {
        Value = value;
    }
    public static MenuItemId CreateUnique()
    {
        return new MenuItemId(Guid.NewGuid());
    }
    public static MenuItemId Create(Guid value)
    {
        return new MenuItemId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    private MenuItemId()
    {

    }

}