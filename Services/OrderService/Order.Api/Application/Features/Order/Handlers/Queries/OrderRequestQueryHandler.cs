//file="OrderRequestQueryHandler.cs" >

namespace Order.Api.Application.Features.Account.Handlers.Queries;

/// <summary>
/// Defines the <see cref="OrdersRequestQueryHandler" />.
/// </summary>
public class OrderRequestQueryHandler : IRequestHandler<GetOrderRequest, GetOrderResponse>
{
    /// <summary>
    /// Defines the userService.
    /// </summary>
    private readonly IOrderRepository orderRepository;

    /// <param name="orderRepository"><see cref="IOrderRepository"/>.</param>
    public OrderRequestQueryHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="GetOrderRequest"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{RegisterResponse}"/>.</returns>
    public async Task<GetOrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
    {
        GetOrderResponse orderResponse = new();

        var order = await this.orderRepository.GetById(request.OrdersId);

        if (order is null)
        {
            orderResponse.MessageSummary!.StatusCode = StatusCodes.Status500InternalServerError;
            orderResponse.MessageSummary!.AddError(ErrorMessages.GenericFailed, MessageDisplayTypes.All, ErrorCodes.GenericFailed);
            return orderResponse;
        }

        orderResponse.MessageSummary!.Add(ResponseMessages.OrderCreatedSuccessfully);

        orderResponse.Order = order;

        return orderResponse;
    }
}
