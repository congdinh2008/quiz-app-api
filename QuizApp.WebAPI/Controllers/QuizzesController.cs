using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;

namespace QuizApp.WebAPI;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuizzesController : ControllerBase
{
    private readonly IMediator mediator;

    public QuizzesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuizViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted)
    {
        var result = await mediator.Send(new GetAllQuizQuery { IncludeDeleted = includeDeleted });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] CreateUpdateQuizCommand command)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await mediator.Send(command);
        return Ok(result);
    }
}
