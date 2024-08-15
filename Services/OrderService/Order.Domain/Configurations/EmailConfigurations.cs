//file="EmailConfigurations.cs" >

namespace Order.Domain.Configurations;

/// <summary>
/// Defines the <see cref="EmailConfigurations" />.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "configuation file")]
public class EmailConfigurations
{
    /// <summary>
    /// Defines the ApiKey.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Defines the ApiKey.
    /// </summary>
    public string? FromAddress { get; set; }

    /// <summary>
    /// Defines the ApiKey.
    /// </summary>
    public string? FromName { get; set; }
}