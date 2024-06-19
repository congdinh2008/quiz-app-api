using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;
using QuizApp.Core;

namespace QuizApp.WebAPI;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuizzesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuizViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted)
    {
        var result = await mediator.Send(new QuizGetAllQuery { IncludeDeleted = includeDeleted });
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(QuizViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var request = new QuizGetByIdQuery { Id = id };
        var result = await mediator.Send(request);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost("search")]
    [ProducesResponseType(typeof(PaginatedResult<QuizViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromBody] QuizSearchQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] QuizCreateUpdateCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await mediator.Send(command);
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool isHardDelete)
    {
        var command = new QuizDeleteByIdCommand { Id = id, IsHardDelete = isHardDelete };
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
