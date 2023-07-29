//-----------------------------------------------------------------------
// <copyright file="BranchController.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Controllers;

/// <summary>
/// AirlineDetailsController <see cref="BranchController"/>
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BranchController : BaseController<BranchController>
{
    public BranchController(IMediator mediator, ILogger<BranchController> logger, IHttpContextAccessor httpContextAccessor)
        : base(mediator, logger, httpContextAccessor)
    {
    }

    /// <summary>
    /// Create user for specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The <see cref="Task{CustomApiResponse}"/>.</returns>
    [HttpGet]
    public async Task<IActionResult> Branch()
    {
        var response = await this.mediator.Send(new GetBranchesRequestQuery());

        return Ok(response);
    }
}