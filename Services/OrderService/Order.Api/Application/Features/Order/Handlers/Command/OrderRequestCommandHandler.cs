//file="OrderRequestCommandHandler.cs" >

namespace Order.Api.Application.Features.Account.Handlers.Command;

/// <summary>
/// Defines the <see cref="OrderRequestCommandHandler" />.
/// </summary>
public class OrderRequestCommandHandler : IRequestHandler<OrdersRequestCommand, GetOrderResponse>
{
    /// <summary>
    /// Defines the userService.
    /// </summary>
    private readonly IOrderRepository orderRepository;

    /// <param name="orderRepository"><see cref="IOrderRepository"/>.</param>
    public OrderRequestCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="OrdersRequestCommand"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{RegisterResponse}"/>.</returns>
    public async Task<GetOrderResponse> Handle(OrdersRequestCommand request, CancellationToken cancellationToken)
    {
        GetOrderResponse orderResponse = new();

        OrderEntity orderEntity = new()
        {
            CustomerId = request.CustomerId,
            ProductId = request.ProductId,
            Description = request.Description,
            Address = request.Address,
            OrderDate = request.OrderDate,
            CreatedByUserId = request.CustomerId,
            CreatedOn = DateTime.UtcNow
        };

        var order = await this.orderRepository.Insert(orderEntity);

        if (order is null)
        {
            orderResponse.MessageSummary!.StatusCode = StatusCodes.Status500InternalServerError;
            orderResponse.MessageSummary!.AddError(ErrorMessages.GenericFailed, MessageDisplayTypes.All, ErrorCodes.GenericFailed);
            return orderResponse;
        }

        orderResponse.Order = order;
        orderResponse.MessageSummary!.Add(ResponseMessages.OrderCreatedSuccessfully);

        // Create events
        OrderCreationIntegrationEvent orderCreationIntegrationEvent = new(order.Id, order.CustomerId, order.ProductId);

        //await publishEndpoint.Publish(orderCreationIntegrationEvent, cancellationToken);

        return orderResponse;
    }
}