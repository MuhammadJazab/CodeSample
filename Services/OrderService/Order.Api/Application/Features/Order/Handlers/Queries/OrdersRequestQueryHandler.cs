//file="OrderRequestQueryHandler.cs" >

namespace Order.Api.Application.Features.Account.Handlers.Queries;

/// <summary>
/// Defines the <see cref="OrdersRequestQueryHandler" />.
/// </summary>
public class OrdersRequestQueryHandler : IRequestHandler<GetAllOrdersRequest, GetAllOrderResponse>
{
    /// <summary>
    /// Defines the userService.
    /// </summary>
    private readonly IOrderRepository orderRepository;

    /// <param name="orderRepository"><see cref="IOrderRepository"/>.</param>
    public OrdersRequestQueryHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="GetAllOrdersRequest"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{RegisterResponse}"/>.</returns>
    public async Task<GetAllOrderResponse> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
    {
        GetAllOrderResponse orderResponse = new();

        var orders = await this.orderRepository.ToListAsync();

        if (orders is null || orders!.Count == 0)
        {
            orderResponse.MessageSummary!.StatusCode = StatusCodes.Status500InternalServerError;
            orderResponse.MessageSummary!.AddError(ErrorMessages.GenericFailed, MessageDisplayTypes.All, ErrorCodes.GenericFailed);
            return orderResponse;
        }

        orderResponse.MessageSummary!.Add(ResponseMessages.OrderCreatedSuccessfully);

        orderResponse.Orders = orders;

        return orderResponse;
    }
}
