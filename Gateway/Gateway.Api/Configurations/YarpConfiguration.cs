namespace Gateway.Api.Configurations;

/// <summary>
/// Defines the <see cref="YarpConfiguration" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class YarpConfiguration
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

