using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TurqoiseEatary.Application.Menus.Commands.CreateMenu;
using TurqoiseEatary.Contracts.Menus;
using TurqoiseEatary.Domain.Host;
using TurqoiseEatary.Domain.Menu;

namespace TurqoiseEatary.Api.Controllers;

[Route("hosts/{hostId}/menu")]
[ApiController]
public class MenuController : ApiController
// public class MenuController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    public MenuController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateMenu(
        [FromBody] CreateMenuRequest request,
        [FromRoute] string hostId
        )
    {

        var command = _mapper.Map<CreateMenuCommand>((request, hostId));
        var createMenuResult = await _mediator.Send(command);

        var createdMenuResult = await _mediator.Send(command);
        return createdMenuResult.Match(
            menu => Ok(_mapper.Map<MenuResponse>(menu)),
            errors => Problem(errors)
        );
    }
}