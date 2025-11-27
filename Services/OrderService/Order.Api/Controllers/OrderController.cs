//file="OrderController.cs" >

namespace Order.Api.Controllers;

/// <summary>
/// Defines the <see cref="OrderController" />.
/// </summary>
/// <remarks>
///  Initializes a new instance of the <see cref="OrderController"/> class.
/// </remarks>
/// <param name="mediator"></param>
/// <param name="logger"></param>
/// <param name="httpContextAccessor"></param>
[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator, ILogger<OrderController> logger, IHttpContextAccessor httpContextAccessor) : BaseController<OrderController>(mediator, logger, httpContextAccessor)
{

    /// <summary>
    /// Get all Orders request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>.</returns>
    [HttpGet(HttpVerbConstants.Orders)]
    public async Task<IActionResult> Order()
    {
        GetAllOrderResponse orderResponse = await mediator.Send(new GetAllOrdersRequest());

        return orderResponse is not null ? Ok(orderResponse) : BadRequest(orderResponse);
    }

    /// <summary>
    /// Get all Orders request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>.</returns>
    [HttpGet(HttpVerbConstants.OrderById)]
    public async Task<IActionResult> Order(Guid id)
    {
        GetOrderResponse orderResponse = await mediator.Send(new GetOrderRequest(orderId: id));

        return orderResponse is not null ? Ok(orderResponse) : BadRequest(orderResponse);
    }

    /// <summary>
    /// Create Order request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>.</returns>
    [HttpPost(HttpVerbConstants.Orders)]
    public async Task<IActionResult> Order([FromBody] OrderRequest request)
    {
        GetOrderResponse orderResponse = await mediator.Send(new OrdersRequestCommand(orderRequest: request));

        return orderResponse is not null ? Ok(orderResponse) : BadRequest(orderResponse);
    }
}