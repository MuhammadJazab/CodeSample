//file="OrderRequest.cs" >

namespace Order.Api.Application.Models.Requests;

/// <summary>
/// Defines the <see cref="OrderRequest" />.
/// </summary>
public class OrderRequest
{
    /// <summary>
    /// Gets or sets the CustomerId.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the ProductId.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the Address.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the OrderDate.
    /// </summary>
    public DateTime OrderDate { get; set; } = DateTime.MinValue;
}