//file="OrderCreationIntegratoinEventHandler.cs" >

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Notification.Api.IntegrationEvents.EventHandlers;

/// <summary>
/// Defines the <see cref="OrderCreationIntegratoinEventHandler" />.
/// </summary>
public class OrderCreationIntegratoinEventHandler : BackgroundService
{
    private readonly string _hostname;
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public OrderCreationIntegratoinEventHandler(IConfiguration configuration)
    {
        _hostname = configuration["RabbitMQ:HostName"];
        _queueName = configuration["RabbitMQ:QueueName"];

        var factory = new ConnectionFactory() { HostName = _hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreationEvent>(message);

            // Mock notification
            Console.WriteLine($"Order {orderCreatedEvent.Id} for user {orderCreatedEvent.CustomerId} was created. Sending notification...");
        };
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
