//file="OrderCreationIntegrationEvent.cs" >

namespace Order.Api.IntegrationEvents.Events;

/// <summary>
/// Defines the <see cref="OrderCreationIntegrationEvent" />.
/// </summary>
public record OrderCreationIntegrationEvent
{
    /// <summary>
    /// order id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Customer Id
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Product Id
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Define the OrderCreationIntegrationEvent
    /// </summary>
    /// <param name="id"><see cref="Guid"/></param>
    /// <param name="customerId"><see cref="Guid"/></param>
    /// <param name="productId"><see cref="Guid"/></param>
    public OrderCreationIntegrationEvent(Guid id, Guid customerId, Guid productId)
    {
        this.Id = id;
        this.CustomerId = customerId;
        this.ProductId = productId;
    }
}
