//-----------------------------------------------------------------------
// <copyright file="JwtSettings.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Domain.Configurations;

/// <summary>
/// Defines the <see cref="JwtSettings" />.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JwtSettings"/> class.
    /// When we inject in the service configutaion it requires default constructor.
    /// </summary>
    public JwtSettings()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtSettings"/> class.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="durationInMinutes"></param>
    public JwtSettings(string key, string issuer, string audience, double durationInMinutes)
    {
        Key = key;
        Issuer = issuer;
        Audience = audience;
        DurationInMinutes = durationInMinutes;
    }

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
}