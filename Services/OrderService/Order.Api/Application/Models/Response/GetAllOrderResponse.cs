
//file="GetAllOrderResponse.cs" >

namespace Order.Api.Application.Models.Response;

/// <summary>
/// Defines the <see cref="GetAllOrderResponse" />.
/// </summary>
public class GetAllOrderResponse : BaseResponse
{
    public List<OrderEntity> Orders { get; set; }

    public GetAllOrderResponse()
    {
        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllOrderResponse"/> class.
    /// </summary>
    public GetAllOrderResponse(List<OrderEntity> orders)
    {
        this.Orders = orders;

        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }
}

