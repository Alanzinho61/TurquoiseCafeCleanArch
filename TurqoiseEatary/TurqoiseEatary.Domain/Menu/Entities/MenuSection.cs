using TurqoiseEatary.Domain.Common.Models;
using TurqoiseEatary.Domain.Common.Menu.ValueObjects;

namespace TurqoiseEatary.Domain.Menu.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = new();
    public string Name { get; }
    public string Description { get; }

    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();
    private MenuSection(
        MenuSectionId menuSectionId,
        string name,
        string description,
        List<MenuItem> items) : base(menuSectionId)
    {

        Name = name;
        Description = description;
        _items = items;
    }

    public static MenuSection Create(
        string name,
        string description,
        List<MenuItem> items

    )
    {

        return new(
            MenuSectionId.CreateUnique(),
            name,
            description,
            items
        );
    }
    private MenuSection()
    {

    }

}