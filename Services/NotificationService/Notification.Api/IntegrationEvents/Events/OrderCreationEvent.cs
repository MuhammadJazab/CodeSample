//file="OrderCreationEvent.cs" >

namespace Notification.Api.IntegrationEvents.Events;

/// <summary>
/// Defines the <see cref="OrderCreationEvent" />.
/// </summary>
public record OrderCreationEvent
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
    public OrderCreationEvent(Guid id, Guid customerId, Guid productId)
    {
        this.Id = id;
        this.CustomerId = customerId;
        this.ProductId = productId;
    }
}
