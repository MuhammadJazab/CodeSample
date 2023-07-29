//-----------------------------------------------------------------------
// <copyright file="GrpcConfiguration.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Configurations;

/// <summary>
/// Defines the <see cref="GrpcConfiguration"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class GrpcConfiguration
{
    /// <summary>
    /// Gets or sets the Authorization Header.
    /// </summary>
    public string? AuthorizationHeader { get; set; }

    /// <summary>
    /// Gets or sets the Authorization Api Url.
    /// </summary>
    public string? GrpcServiceUrl { get; set; }
}

