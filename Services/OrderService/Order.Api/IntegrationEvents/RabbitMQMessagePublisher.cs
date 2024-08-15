using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Order.Api.IntegrationEvents;

public class RabbitMQMessagePublisher : IMessagePublisher
{
    private readonly string _hostname;
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQMessagePublisher(IConfiguration configuration)
    {
        _hostname = configuration["RabbitMQ:HostName"];
        _queueName = configuration["RabbitMQ:QueueName"];

        var factory = new ConnectionFactory() { HostName = _hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public async Task PublishAsync<T>(T message)
    {
        var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: messageBody);
        await Task.CompletedTask;
    }
}
