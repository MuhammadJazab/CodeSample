//-----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Api.Controllers;

/// <summary>
/// Defines the <see cref="BaseController{T}" />.
/// </summary>
/// <typeparam name="T">Get the generic type</typeparam>
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

    /// <summary>
    /// Initialises a new instance of the <see cref="BaseController"/> class.
    /// </summary>
    /// <param name="mediator">mediator instance</param>
    /// <param name="logger">logger instance for log</param>
    /// <param name="httpContextAccessor">httpContextAccessor for http context</param>
    public BaseController(IMediator mediator, ILogger<T> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.mediator = mediator;
        this.logger = logger;
        this.httpContextAccessor = httpContextAccessor;
    }
}