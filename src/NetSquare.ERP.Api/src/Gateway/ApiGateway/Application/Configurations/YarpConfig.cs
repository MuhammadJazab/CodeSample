//-----------------------------------------------------------------------
// <copyright file="YarpConfig.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Application.Configurations;

/// <summary>
/// Defines the<see cref="YarpConfig" />.
/// </summary>
public class YarpConfig
{
    /// <summary>
    /// Gets or sets the routes.
    /// </summary>
    public List<RouteConfig>? Routes { get; set; }

    /// <summary>
    /// Gets or sets the clusters.
    /// </summary>
    public List<ClusterConfig>? Clusters { get; set; }
}