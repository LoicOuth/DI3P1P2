using Application.BillingStates.Commands.UpdateBillingStates;
using Application.BillingStates.Queries.GetBillingStateQuery;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BillingStateController : ControllerBase
{
    private readonly IMediator _mediator;

    public BillingStateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{franchiseId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBillingState(string franchiseId)
    {
        try
        {
            var result = await _mediator.Send(new GetBillingStateQuery(franchiseId));
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

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBillingState(UpdateBillingStateRequest request)
    {
        try
        {
            var result = await _mediator.Send(new UpdateBillingStatesCommand(request.FranchiseId, request.Enable));
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
