//file="GetOrderRequest.cs" >

namespace Order.Api.Application.Features.Account.Requests.Queries;

/// <summary>
/// Defines the <see cref="GetAllOrdersRequest" />.
/// </summary>
public class GetOrderRequest : IRequest<GetOrderResponse>
{
    public Guid OrdersId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllOrderResponse"/> class.
    /// </summary>
    public GetOrderRequest(Guid orderId)
    {
        this.OrdersId = orderId;
    }
}