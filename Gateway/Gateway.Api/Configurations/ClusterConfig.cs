namespace Gateway.Api.Configurations;

/// <summary>
/// Defines the <see cref="ClusterConfig" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class ClusterConfig
{
    /// <summary>
    /// Gets or sets the cluster identifier.
    /// </summary>
    public string? ClusterId { get; set; }

    /// <summary>
    /// Gets or sets the destinations.
    /// </summary>
    public List<DestinationConfig>? Destinations { get; set; }
}