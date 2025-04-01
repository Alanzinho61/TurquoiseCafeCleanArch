using ErrorOr;
using MediatR;
using TurqoiseEatary.Application.Common.Interfaces;
using TurqoiseEatary.Application.Menus.Commands.CreateMenu;
using TurqoiseEatary.Domain.Common.Menu;
using TurqoiseEatary.Domain.Host;
using TurqoiseEatary.Domain.Menu;
using TurqoiseEatary.Domain.Menu.Entities;

namespace TurqoiseEatary.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;
    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var menu = Menu.Create(
            hostId: HostId.Create(Guid.Parse(request.HostId)),
            name: request.Name,
            description: request.Description,
            sections: request.Sections.ConvertAll(section => MenuSection.Create(
                section.Name,
                section.Description,
                section.Items.ConvertAll(item => MenuItem.Create(
                    item.Name,
                    item.Description
                ))
            ))
        );

        _menuRepository.add(menu);
        return menu;
    }
}

