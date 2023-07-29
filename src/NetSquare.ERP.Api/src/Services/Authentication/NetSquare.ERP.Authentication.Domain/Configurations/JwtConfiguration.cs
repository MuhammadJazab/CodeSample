//-----------------------------------------------------------------------
// <copyright file="JwtConfiguration.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Configurations;

/// <summary>
/// Defines the <see cref="JwtConfiguration" />.
/// </summary>
public class JwtConfiguration
{
    /// <summary>
    /// Gets or sets the Key.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Issuer.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Audience.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the DurationInMinutes.
    /// </summary>
    public double DurationInMinutes { get; set; }
}