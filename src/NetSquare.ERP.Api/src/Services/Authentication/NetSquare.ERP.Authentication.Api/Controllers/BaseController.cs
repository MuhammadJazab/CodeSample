//-----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Controllers;

/// <summary>
/// Defines the <see cref="BaseController{T}" />.
/// </summary>
/// <typeparam name="T">.</typeparam>
public class BaseController<T> : ControllerBase where T : BaseController<T>
{
    /// <summary>
    /// Defines the mediator.
    /// </summary>
    protected readonly IMediator mediator;

    /// <summary>
    /// Defines the logger.
    /// </summary>
    protected readonly ILogger<T> logger;

    /// <summary>
    /// Defines the httpContextAccessor.
    /// </summary>
    protected readonly IHttpContextAccessor httpContextAccessor;


    public BaseController(IMediator mediator, ILogger<T> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.mediator = mediator;
        this.logger = logger;
        this.httpContextAccessor = httpContextAccessor;
    }
}