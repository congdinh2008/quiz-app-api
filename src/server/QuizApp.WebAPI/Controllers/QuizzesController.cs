using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;
using QuizApp.Core;

namespace QuizApp.WebAPI;

/// <summary>
/// Quizzes controller
/// </summary>
/// <param name="mediator"></param>
[Authorize]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class QuizzesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    /// <summary>
    /// Get all quizzes.
    /// </summary>
    /// <param name="includeDeleted">Boolean value specifying whether to include deleted records.</param>
    /// <returns>List of quizzes.</returns>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuizViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted)
    {
        var result = await mediator.Send(new QuizGetAllQuery { IncludeDeleted = includeDeleted });
        return Ok(result);
    }

    /// <summary>
    /// Get a quiz by its ID.
    /// </summary>
    /// <param name="id">The ID of the quiz.</param>
    /// <returns>The quiz with the specified ID.</returns>
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

    /// <summary>
    /// Search for quizzes based on a query.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <returns>A paginated result of quizzes.</returns>
    [AllowAnonymous]
    [HttpPost("search")]
    [ProducesResponseType(typeof(PaginatedResult<QuizViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromBody] QuizSearchQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create a new quiz.
    /// </summary>
    /// <param name="command">The command to create the quiz.</param>
    /// <returns>A boolean indicating whether the quiz was successfully created.</returns>
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

    /// <summary>
    /// Delete a quiz by its ID.
    /// </summary>
    /// <param name="id">The ID of the quiz to delete.</param>
    /// <param name="isHardDelete">Boolean value specifying whether to perform a hard delete.</param>
    /// <returns>A boolean indicating whether the quiz was successfully deleted.</returns>
    /// [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool isHardDelete)
    {
        var command = new QuizDeleteByIdCommand { Id = id, IsHardDelete = isHardDelete };
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
