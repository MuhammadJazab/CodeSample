namespace Order.Api.IntegrationEvents;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message);
}
