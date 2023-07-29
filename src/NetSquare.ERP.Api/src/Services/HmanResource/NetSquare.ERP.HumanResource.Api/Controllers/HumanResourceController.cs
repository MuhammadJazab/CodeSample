//-----------------------------------------------------------------------
// <copyright file="HumanResourceController.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Api.Controllers;

/// <summary>
/// AirlineDetailsController <see cref="HumanResourceController"/>
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class HumanResourceController : BaseController<HumanResourceController>
{
    public HumanResourceController(IMediator mediator, ILogger<HumanResourceController> logger, IHttpContextAccessor httpContextAccessor)
        : base(mediator, logger, httpContextAccessor)
    {
    }

    /// <summary>
    /// Get all attandencs.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The <see cref="Task{CustomApiResponse}"/>.</returns>
    ////[HttpGet]
    ////public async Task<IActionResult> UserManagement([FromBody] CreateUserRequest request)
    ////{
    ////    CreateUserRequestCommand command = new(request);

    ////    var response = await this.mediator.Send(command);
    ////    return
    ////}
}

