//-----------------------------------------------------------------------
// <copyright file="JwtConfigurations.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Application.Configurations;

/// <summary>
/// Defines the <see cref="JwtConfigurations" />.
/// </summary>
public class JwtConfigurations
{
    /// <summary>
    /// Gets or sets the Key.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// Gets or sets the Issuer.
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Gets or sets the Audience.
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Gets or sets the DurationInMinutes.
    /// </summary>
    public double DurationInMinutes { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtConfigurations"/> class.
    /// When we inject in the service configutaion it requires default constructor.
    /// </summary>
    public JwtConfigurations()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtSettings"/> class.
    /// </summary>
    /// <param name="key">The object of string</param>
    /// <param name="issuer">The object of string</param>
    /// <param name="audience">The object of string</param>
    /// <param name="durationInMinutes">The object of double</param>
    public JwtConfigurations(string key, string issuer, string audience, double durationInMinutes)
    {
        Key = key;
        Issuer = issuer;
        Audience = audience;
        DurationInMinutes = durationInMinutes;
    }
}