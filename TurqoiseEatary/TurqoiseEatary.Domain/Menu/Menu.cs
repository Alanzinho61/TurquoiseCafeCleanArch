using System.ComponentModel;
using TurqoiseEatary.Domain.Menu.Entities;
using TurqoiseEatary.Domain.Common.Menu.ValueObjects;
using TurqoiseEatary.Domain.Common.Models;

namespace TurqoiseEatary.Domain.Common.Menu;

public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = new();
    public string Name { get; }
    public string Description { get; }
    public float AvregeRating { get; }

    private Menu(
        MenuId menuId,
        string name,
        string description) : base(menuId)
    {
        Name = name;
        Description = description;
    }

    public static Menu Create(
        string name,
        string description)
    {
        return new(
            MenuId.CreateUnique(),
            name,
            description
        );
    }


}