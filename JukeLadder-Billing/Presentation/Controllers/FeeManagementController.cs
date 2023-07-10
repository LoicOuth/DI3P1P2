using Application.FeeManagement.Command.SetFeeCommand;
using Application.FeeManagement.Queries.GetLastFeeQuery;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]

    public class FeeManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeeManagementController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetFee([FromBody] SetFeeRequest request)
        {
            try
            {
                var result = await _mediator.Send(new SetFeeCommand(request.min, request.max));
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

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLastFee()
        {
            try
            {
                var result = await _mediator.Send(new GetLastFeeQuery());
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
}
