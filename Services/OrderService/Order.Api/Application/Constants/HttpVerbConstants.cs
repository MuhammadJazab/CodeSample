//file="HttpVerbConstants.cs" >

namespace Order.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="HttpVerbConstants" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class HttpVerbConstants
{
    /// <summary>
    /// Defines the Orders.
    /// </summary>
    public const string Orders = "orders";

    /// <summary>
    /// Defines the OrderById.
    /// </summary>
    public const string OrderById = "orders/{id}";
}
