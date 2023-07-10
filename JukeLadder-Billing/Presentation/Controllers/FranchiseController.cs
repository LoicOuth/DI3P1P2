using Application.Franchises.Command.CreateFranchiseCommand;
using Application.Franchises.Command.DeleteFranchiseCommand;
using Application.Franchises.Command.UpdateFranchiseCommand;
using Application.Franchises.Queries.FranchisesCountQuery;
using Application.Franchises.Queries.GetAllFranchiseQuery;
using Application.Franchises.Queries.GetFranchiseByUserEmailQuery;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class FranchiseController : ControllerBase
    {
		private readonly IMediator _mediator;
		public FranchiseController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var result = await _mediator.Send(new GetAllFranchiseQuery());
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
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _mediator.Send(new Application.Franchises.Queries.GetFranchiseByIdQuery.GetFranchiseByIdQuery(id));
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

        [HttpGet("email")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFranchiseByUserEmail()
        {
            try
            {
                string userEmail = HttpContext.Request.Query.FirstOrDefault(x => x.Key == "email").Value.ToString();
                var result = await _mediator.Send(new GetFranchiseByUserEmailQuery(userEmail));
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
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Post([FromBody] CreateFranchiseRequest request)
		{
			try
			{
				var result = await _mediator.Send(new CreateFranchiseCommand(request.UserId, request.Name));
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
		[HttpPut("{franchiseId}")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update([FromBody] UpdateFranchiseRequest request, string franchiseId)
		{
			try
			{
				var result = await _mediator.Send(new UpdateFranchiseCommand(franchiseId, request.UserId, request.Name, request.Theme));
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

		[HttpDelete("{franchiseId}")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Delete(string franchiseId)
		{
			try
			{
				var result = await _mediator.Send(new DeleteFranchiseCommand(franchiseId));
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

        [HttpGet("count")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetCount()
        {
            try
            {
                var result = await _mediator.Send(new FranchisesCountQuery());
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
