using Application.PlaylistStates.Command.UpdatePlaylistStateCommand;
using Application.PlaylistStates.Queries.GetPlaylistStateQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PlaylistStateController : ControllerBase
{
    private readonly IMediator _mediator;
    public PlaylistStateController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{franchiseId}")]
    public async Task<IActionResult> GetStateForFranchiseId(string franchiseId)
    {
        var state = await _mediator
            .Send(new GetPlaylistStateQuery(franchiseId));
        
        return Ok(state);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateState(UpdatePlaylistStateRequest request)
    {
        var state = await _mediator
            .Send(new UpdatePlaylistStateCommand(request.FranchiseId, request.Enable));
        
        return Ok(state);
    }
}
