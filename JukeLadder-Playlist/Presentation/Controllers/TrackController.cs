using Application.Common.Exceptions;
using Application.PlaylistStates.Queries.GetPlaylistStateQuery;
using Application.Tracks.Commands.AddTrackPlaylistCommand;
using Application.Tracks.Commands.DeleteTrackWithIdCommand;
using Application.Tracks.Commands.EndTrackCommand;
using Application.Tracks.Commands.UpdateTrackCurrentDurationCommand;
using Application.Tracks.Commands.UpdateTrackPositionCommand;
using Application.Tracks.Commands.VoteTrackCommand;
using Application.Tracks.Queries.GetTrackWithFranchiseIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TrackController : ControllerBase
{
    private readonly IMediator _mediator;
    public TrackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{franchiseId}")]
    public async Task<IActionResult> GetTracksWithFranchiseId(string franchiseId)
    {
        try
        {
            var state = await _mediator.Send(new GetPlaylistStateQuery(franchiseId));

            if(!state)
                return BadRequest("The playlist is not active");

            var result = await _mediator.Send(new GetTrackWithFranchiseIdQuery(franchiseId));
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddTrackPlaylist([FromBody] AddTrackPlaylistRequest request)
    {
        try
        {
            await _mediator.Send(new AddTrackPlaylistCommand(request.FranchiseId, request.Id, request.Title, request.Artist, request.Album, request.Cover, request.Duration));
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch(AlreadyExistException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> VoteTrackLike([FromBody] VoteTrackLikeRequest request)
    {
        try
        {
            return Ok(await _mediator.Send(new VoteTrackCommand(request.TrackId, request.Identifier, request.Action)));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }


    [HttpPut("{TrackId}")]
    public async Task<IActionResult> UpdateCurrentDuration([FromBody] UpdateDurationRequest request, string TrackId)
    {
        try
        {
            return Ok(await _mediator.Send(new UpdateTrackCurrentDurationCommand(TrackId, request.NewDuration)));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpDelete("{trackId}")]
    public async Task<IActionResult> DeleteTrack(string trackId)
    {
        try
        {
            return Ok(await _mediator.Send(new DeleteTrackWithIdCommand(trackId)));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPost("{trackId}/end")]

    public async Task<IActionResult> EndTrack([FromBody] EndTrackRequest request, string trackId)
    {
        try
        {
            return Ok(await _mediator.Send(new EndTrackCommand(trackId, request.FranchiseId)));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }

    [HttpPut("{trackId}/position")]
    public async Task<IActionResult> UpdateCurrentDuration([FromBody] UpdatePositionRequest request, string trackId)
    {
        try
        {
            return Ok(await _mediator.Send(new UpdateTrackPositionCommand(request.NewPosition, trackId)));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
    }
}
