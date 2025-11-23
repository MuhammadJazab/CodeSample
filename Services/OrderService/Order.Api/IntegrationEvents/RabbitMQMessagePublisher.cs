namespace Order.Api.IntegrationEvents;

public class RabbitMQMessagePublisher : IMessagePublisher
{
    private readonly string hostname;
    private readonly string queueName;
    private readonly IConnection connection;
    private readonly IModel channel;

    public RabbitMQMessagePublisher(IConfiguration configuration)
    {
        hostname = configuration["RabbitMQ:HostName"]!;
        queueName = configuration["RabbitMQ:QueueName"]!;

        var factory = new ConnectionFactory() { HostName = hostname };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public async Task PublishAsync<T>(T message)
    {
        var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: messageBody);
        await Task.CompletedTask;
    }
}
