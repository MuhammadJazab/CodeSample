
//file="UserController.cs" >

namespace User.Api.Controllers;

/// <summary>
/// Defines the <see cref="UserController" />.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserController"/> class.
/// </remarks>
/// <param name="loginService">The login service.</param>
[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator, ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
    : BaseController<UserController>(mediator, logger, httpContextAccessor)
{

    /// <summary>
    /// Get Users.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>.</returns>
    [HttpGet(HttpVerbConstants.Users)]
    public async Task<IActionResult> Users()
    {
        UsersResponse usersResponse = await mediator.Send(new UsersRequest());

        return usersResponse is not null ? Ok(usersResponse) : BadRequest(usersResponse);
    }

    /// <summary>
    /// Get User by Id.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>.</returns>
    [HttpGet(HttpVerbConstants.UserById)]
    public async Task<IActionResult> Users(string id)
    {
        UserResponse userResponse = await mediator.Send(new UserRequest(id));

        return userResponse is not null ? Ok(userResponse) : BadRequest(userResponse);
    }

    /// <summary>
    /// Register the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>.</returns>
    [HttpPost(HttpVerbConstants.Users)]
    public async Task<IActionResult> Register([FromBody] Application.Models.Requests.RegisterRequest request)
    {
        RegisterResponse response = await mediator.Send(new RegisterRequestCommand(request));

        if (response.MessageSummary is not null && !response.MessageSummary!.IsValid)
        {
            return StatusCode(response.MessageSummary.StatusCode, response);
        }

        return Ok(response);
    }

    /// <summary>
    /// Update the specified user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>.</returns>
    [HttpPut(HttpVerbConstants.Users)]
    public async Task<IActionResult> Update([FromBody] UpdateRequest request)
    {
        UpdateResponse response = await mediator.Send(new UpdateRequestCommand(request));

        if (response.MessageSummary is not null && !response.MessageSummary!.IsValid)
        {
            return StatusCode(response.MessageSummary.StatusCode, response);
        }

        return Ok(response);
    }
}