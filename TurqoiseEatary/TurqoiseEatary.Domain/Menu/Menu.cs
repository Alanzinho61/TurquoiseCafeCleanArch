using System.ComponentModel;
using TurqoiseEatary.Domain.Menu.Entities;
using TurqoiseEatary.Domain.Common.Menu.ValueObjects;
using TurqoiseEatary.Domain.Common.Models;
using TurqoiseEatary.Domain.Dinner;
using TurqoiseEatary.Domain.MenuReview;
using TurqoiseEatary.Domain.Common.ValueObjects;
using TurqoiseEatary.Domain.Host;

namespace TurqoiseEatary.Domain.Menu;

public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new(); public string Name { get; }
    public string Description { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public HostId HostId { get; private set; }
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
    public DateTime CreateDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    private Menu(
        MenuId menuId,
        HostId hostId,
        string name,
        string description,
        AverageRating averageRating,
        List<MenuSection>? sections) : base(menuId)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        _sections = sections;
        AverageRating = averageRating;
    }

    public static Menu Create(
        HostId hostId,
        string name,
        string description,
        List<MenuSection>? sections = null)
    {
        var menu = new Menu(
             MenuId.CreateUnique(),
             hostId,
             name,
             description,
             AverageRating.CreateNew(0),
             sections ?? new()
         );
        return menu;
    }
    private Menu() { }

}