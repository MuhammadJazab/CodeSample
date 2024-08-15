
//file="GetOrderResponse.cs" >

namespace Order.Api.Application.Models.Response;

/// <summary>
/// Defines the <see cref="GetOrderResponse" />.
/// </summary>
public class GetOrderResponse : BaseResponse
{
    public OrderEntity Order { get; set; }

    public GetOrderResponse()
    {
        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetOrderResponse"/> class.
    /// </summary>
    public GetOrderResponse(OrderEntity order)
    {
        this.Order = order;

        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }
}

