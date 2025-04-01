using Mapster;
using TurqoiseEatary.Application.Menus.Commands.CreateMenu;
using TurqoiseEatary.Contracts.Menus;
using TurqoiseEatary.Domain.Menu;
using MenuSection = TurqoiseEatary.Domain.Menu.Entities.MenuSection;
using MenuItem = TurqoiseEatary.Domain.Menu.Entities.MenuItem;
using TurqoiseEatary.Domain.Dinner;
namespace TurqoiseEatary.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
        .Map(dest => dest.HostId, src => src.HostId)
        .Map(dest => dest, src => src.Request);

        config.NewConfig<Menu, MenuResponse>()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
        .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
        .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(dinnerId => dinnerId.Value))
        .Map(dest => dest.MenuRewiewIds, src => src.MenuReviewIds.Select(menuReviewId => menuReviewId.Value));

        config.NewConfig<MenuSection, MenuSectionResponse>()
        .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<MenuItem, MenuSectionResponse>()
        .Map(dest => dest.Id, src => src.Id.Value);


    }

}