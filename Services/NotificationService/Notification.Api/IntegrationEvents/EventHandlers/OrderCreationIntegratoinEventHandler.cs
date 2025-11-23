//file="OrderCreationIntegratoinEventHandler.cs" >

namespace Notification.Api.IntegrationEvents.EventHandlers;

/// <summary>
/// Defines the <see cref="OrderCreationIntegratoinEventHandler" />.
/// </summary>
public class OrderCreationIntegratoinEventHandler : BackgroundService
{
    private readonly string hostname;
    private readonly string queueName;
    private readonly IConnection connection;
    private readonly IModel channel;

    public OrderCreationIntegratoinEventHandler(IConfiguration configuration)
    {
        hostname = configuration["RabbitMQ:HostName"]!;
        queueName = configuration["RabbitMQ:QueueName"]!;

        var factory = new ConnectionFactory() { HostName = hostname };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreationEvent>(message);

            if (orderCreatedEvent == null) return;

            // Mock notification
            Console.WriteLine($"Order {orderCreatedEvent.Id} for user {orderCreatedEvent.CustomerId} was created. Sending notification...");
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
