using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business;

namespace QuizApp.WebAPI;

/// <summary>
/// Auth controller
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Login a user.   
    /// </summary>
    /// <param name="request">Login information with username and password</param>
    /// <returns>Login result view model with access token and user information</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResultViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] UserLoginQuery request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(request);

        return Ok(result);
    }
}
