using Application.Track.Command.AddTracksCommand;
using Application.Track.Queries.GetSuggestionsQuery;
using Application.Track.Queries.GetTrackFromQuery;
using Domain.Enums;

namespace Presentation.Controllers.V1;

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

    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTracks()
    {
        try
        {
            string value = HttpContext.Request.Query.FirstOrDefault(x => x.Key == "value").Value.ToString();
            string genre = HttpContext.Request.Query.FirstOrDefault(x => x.Key == "genre").Value.ToString();
            string franchiseId = HttpContext.Request.Query.FirstOrDefault(x => x.Key == "franchiseId").Value.ToString();

            if (!Enum.TryParse<SolrFields>(
                HttpContext.Request.Query.FirstOrDefault(x => x.Key == "field").Value.ToString(),
                true,
                out SolrFields field))
                return BadRequest();
            
            var result = await _mediator.Send(new GetTrackFromQuery(field, value, genre, franchiseId));
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

    [HttpGet("suggestions")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSuggestions()
    {
        try
        {
            string query = HttpContext.Request.Query.FirstOrDefault(x => x.Key == "query").Value.ToString();

            var result = await _mediator.Send(new GetSuggestionsQuery(query));
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

    [HttpPost()]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddTracks([FromBody] AddTracksRequest request)
    {
        try
        {
            var result = await _mediator.Send(new AddTracksCommand(request.IdPlaylist, request.FranchiseId));
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
}
