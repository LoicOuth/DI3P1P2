using Application.BillingStates.Queries.GetBillingStateQuery;
using Application.PaymentIntent.Command.CreatePaymentIntentWithProductIdCommand;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PaymentIntendController : ControllerBase
{
	private readonly IMediator _mediator;
    public PaymentIntendController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	[Consumes(MediaTypeNames.Application.Json)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] CreatePaymentIntentRequest request)
	{
		try
		{
			var state = await _mediator.Send(new GetBillingStateQuery(request.FranchiseId));

			if (!state)
                return BadRequest("Billing is not active");

            var result = await _mediator.Send(new CreatePaymentIntentWithProductIdCommand(request.ProductId, request.FranchiseId, request.Title, request.Artist, request.Album, request.Cover, request.Duration, request.Id));
            return Ok(result.ClientSecret);
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
