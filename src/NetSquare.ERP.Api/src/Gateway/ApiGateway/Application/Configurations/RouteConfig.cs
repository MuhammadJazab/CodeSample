//-----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Application.Configurations;

/// <summary>
/// Defines the <see cref="RouteConfig" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class RouteConfig
{
    /// <summary>
    /// Gets or sets the route identifier.
    /// </summary>
    public string? RouteId { get; set; }

    /// <summary>
    /// Gets or sets the cluster identifier.
    /// </summary>
    public string? ClusterId { get; set; }

    /// <summary>
    /// Gets or sets the match.
    /// </summary>
    public RouteMatch? Match { get; set; }
}