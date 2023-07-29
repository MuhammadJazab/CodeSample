//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Controllers;

/// <summary>
/// Defines the <see cref="AccountController" />.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController<AccountController>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="loginService">The login service.</param>
    public AccountController(IMediator mediator, ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
        : base(mediator, logger, httpContextAccessor) { }

    /// <summary>
    /// Logins the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> AccountLogin([FromBody] AuthenticationRequest request)
    {
        AuthenticationResponse loginResponse = await mediator.Send(new UserLoginRequestQuery(email: request.Email, password: request.Password));

        return loginResponse is not null ? Ok(loginResponse) : BadRequest(loginResponse);
    }

    /// <summary>
    /// Logins the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    [HttpPost(HttpVerbConstants.Register)]
    public async Task<IActionResult> AccountRegisteration([FromBody] RegisterUserRequest request)
    {
        var response = await mediator.Send(new RegisterUserRequestCommand(request));

        if (response.MessageSummary is not null && !response.MessageSummary!.IsValid)
        {
            return StatusCode(response.MessageSummary.StatusCode, response);
        }

        return Ok(response);
    }

    /// <summary>
    /// Logins the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    [HttpGet]
    public async Task<IActionResult> Account()
    {
        List<GetUsersResponse> getUsersResponse = await mediator.Send(new GetUsersRequestQuery());

        return Ok(getUsersResponse);
    }
}