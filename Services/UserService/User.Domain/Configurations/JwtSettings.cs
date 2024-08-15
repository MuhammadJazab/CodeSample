//file="JwtSettings.cs" >

namespace User.Domain.Configurations;

/// <summary>
/// Defines the <see cref="JwtSettings" />.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "EF configuation file")]
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
    /// <param name="key">The object of string</param>
    /// <param name="issuer">The object of string</param>
    /// <param name="audience">The object of string</param>
    /// <param name="durationInMinutes">The object of double</param>
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