namespace Gateway.Api.Configurations;

/// <summary>
/// Defines the <see cref="DestinationConfig" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class DestinationConfig
{
    /// <summary>
    /// Gets or sets the destination identifier.
    /// </summary>
    public string? DestinationId { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    public string? Address { get; set; }
}