using Application.Deezer.Queries.SearchAllGenre;
using Application.Deezer.Queries.SearchPlaylistQuery;

namespace Presentation.Controllers.V1;
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class DeezerController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeezerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("search/playlist")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchPlaylist()
    {
        try
        {
            var query = HttpContext.Request.Query.FirstOrDefault(x => x.Key == "query").Value.ToString();
            var result = await _mediator.Send(new SearchPlaylistQuery(query));
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

    [HttpGet("genres")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SearchAllGenre()
    {
        try
        {
            var result = await _mediator.Send(new SearchAllGenreQuery());
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
