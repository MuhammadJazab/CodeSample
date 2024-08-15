//file="RegisterRequestCommand.cs" >

namespace Order.Api.Application.Features.Account.Requests.Command;

/// <summary>
/// Defines the <see cref="OrdersRequestCommand" />.
/// </summary>
public class OrdersRequestCommand : IRequest<GetOrderResponse>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserRequestCommand"/> class.
    /// </summary>
    /// <param name="orderRequest"><see cref="RegisterUserRequest"/></param>
    public OrdersRequestCommand(OrderRequest orderRequest)
    {
        this.CustomerId = orderRequest.CustomerId;
        this.ProductId = orderRequest.ProductId;
        this.Address = orderRequest.Address;
        this.Description = orderRequest.Description;
        this.OrderDate = orderRequest.OrderDate;
    }
}