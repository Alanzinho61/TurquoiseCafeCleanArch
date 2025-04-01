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
    private readonly List<MenuReviewId> _menuReviewIds = new();
    public string Name { get; }
    public string Description { get; }
    public AverageRating AverageRating { get; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public HostId HostId { get; }
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
    public DateTime CreateDateTime { get; }
    public DateTime UpdatedDateTime { get; }
    private Menu(
        MenuId menuId,
        string name,
        string description,
        HostId hostId,
        DateTime createdDateTime,
        DateTime updatedDateTime
        ) : base(menuId)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreateDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Menu Create(
        string name,
        string description,
        HostId hostId,
        List<MenuSection> sections)
    {
        return new(
            MenuId.CreateUnique(),
            name,
            description,
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }


}