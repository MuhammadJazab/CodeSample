
//file="BaseResponse.cs" >



namespace Order.Domain.Common;
/// <summary>
/// Defines the <see cref="BaseResponse" />.
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class BaseResponse
{
    /// <summary>
    /// Gets or sets the MessageSummary.
    /// </summary>
    public MessagesSummary? MessageSummary { get; set; }
}

