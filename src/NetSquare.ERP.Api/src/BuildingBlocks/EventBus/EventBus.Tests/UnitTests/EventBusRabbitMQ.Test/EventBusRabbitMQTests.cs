//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBusRabbitMQ.Test;

/// <summary>
/// Defines the <see cref="EventBusRabbitMQTests " />.
/// </summary>
[TestClass]
public class EventBusRabbitMQTests
{
    /// <summary>
    /// Defines the rabbitMqConnection
    /// </summary>
    private Mock<IRabbitMQConnection>? rabbitMqConnectionMock;

    /// <summary>
    /// Defines the loggerMock.
    /// </summary>
    private Mock<ILogger<NetSquare.ERP.EventBusRabbitMQ.EventBusRabbitMQ>>? loggerMock;

    /// <summary>
    /// Defines the subscriptionsManagerMock
    /// </summary>
    private Mock<IEventBusSubscriptionsManager>? subscriptionsManagerMock;

    /// <summary>
    /// Defines the autofacMock
    /// </summary>
    private Mock<ILifetimeScope>? autofacMock;

    /// <summary>
    /// Defines the rabbitMQClientMock
    /// </summary>
    private Mock<IModel>? rabbitMQClientMock;

    /// <summary>
    /// Defines thebasicPropertiesMock
    /// </summary>
    private Mock<IBasicProperties>? basicPropertiesMock;

    /// <summary>
    /// Defines the connectionFactoryMock
    /// </summary>
    private Mock<IConnectionFactory>? connectionFactoryMock;

    /// <summary>
    /// Defines the connectionMock
    /// </summary>
    private Mock<IConnection>? connectionMock;

    /// <summary>
    /// Defines the EventBusRabbitMQ
    /// </summary>
    private NetSquare.ERP.EventBusRabbitMQ.EventBusRabbitMQ? eventBusRabbitMQSut;

    /// <summary>
    /// Defines the queueName
    /// </summary>
    private const string queueName = "RabbitMQ Test";

    /// <summary>
    /// Defines the retryCount
    /// </summary>
    private const int retryCount = 5;

    /// <summary>
    /// GroupCapacityControllerTests_Init
    /// </summary>
    [TestInitialize]
    public void EventBusRabbitMQTests_Init()
    {
        rabbitMqConnectionMock = new();
        loggerMock = new();
        subscriptionsManagerMock = new();
        autofacMock = new();
        rabbitMQClientMock = new();
        connectionMock = new();
        basicPropertiesMock = new();
        connectionFactoryMock = new();
    }

    /// <summary>
    /// Given_Constructor_When_Initialized_Then_Create_EventBusRabbitMQ_Object
    /// </summary>
    [TestMethod]
    public void Given_Constructor_When_Initialized_Then_Create_EventBusRabbitMQ_Object()
    {
        // ARRANGE
        rabbitMqConnectionMock?.Setup(x => x.IsConnected).Returns(true);
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);
        rabbitMQClientMock?.Setup(x => x.ExchangeDeclare(RabbitMQConstants.BrokerName, ExchangeType.Fanout, true, false, null));

        // ACT
        eventBusRabbitMQSut = new(
            persistentConnection: rabbitMqConnectionMock?.Object!,
            logger: loggerMock?.Object!,
            autofac: autofacMock?.Object!,
            subsManager: subscriptionsManagerMock?.Object!,
            queueName: queueName,
            retryCount: retryCount);

        // ASSERT
        Assert.IsNotNull(eventBusRabbitMQSut);
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_Publish_PublishEvent_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_Publish_PublishEvent_Successfully()
    {
        // ARRANGE 
        rabbitMqConnectionMock?.Setup(x => x.IsConnected).Returns(true);
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);

        eventBusRabbitMQSut = new(
            persistentConnection: rabbitMqConnectionMock?.Object!,
            logger: loggerMock?.Object!,
            autofac: autofacMock?.Object!,
            subsManager: subscriptionsManagerMock?.Object!,
            queueName: queueName,
            retryCount: retryCount);

        TestIntegrationEvent? testIntegrationEvent = default;

        IntegrationEventFixture integrationEventFixture = new(queueName);
        var eventName = integrationEventFixture.GetType().Name;

        var body = JsonSerializer.SerializeToUtf8Bytes(eventName, eventName.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });

        var properties = rabbitMQClientMock?.Object.CreateBasicProperties();

        var policy = RetryPolicy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                this.loggerMock?.Object.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", integrationEventFixture.Id, $"{time.TotalSeconds:n1}", ex.Message);
            });

        rabbitMQClientMock?.Setup(x => x.BasicPublish(RabbitMQConstants.BrokerName, eventName, true, properties, body))
            .Callback<string, string, bool, IBasicProperties, ReadOnlyMemory<byte>>((exchange, routingKey, mandatory, basicProperties, body) =>
            {
                testIntegrationEvent = new(exchange, routingKey, mandatory, basicProperties, body);
            });

        policy.Execute(() =>
        {
            rabbitMQClientMock?.Object.BasicPublish(RabbitMQConstants.BrokerName, eventName, true, properties, body);
        });

        // ACT
        eventBusRabbitMQSut?.Publish(integrationEventFixture);

        // ASSERT
        rabbitMqConnectionMock?.Verify(x => x.IsConnected, Times.AtLeast(1));
        rabbitMqConnectionMock?.Verify(x => x.CreateModel(), Times.AtLeast(1));
        rabbitMQClientMock?.Verify(x => x.BasicPublish(RabbitMQConstants.BrokerName, eventName, true, properties, body), times: Times.AtLeast(1));
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_Try_To_Connect_To_Service_And_Publish_PublishEvent_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_Try_To_Connect_To_Service_And_Publish_PublishEvent_Successfully()
    {
        // ARRANGE 
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);

        connectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);
        connectionFactoryMock?.Setup(x => x.CreateConnection()).Returns(connectionMock?.Object!);

        eventBusRabbitMQSut = new(
            persistentConnection: rabbitMqConnectionMock?.Object!,
            logger: loggerMock?.Object!,
            autofac: autofacMock?.Object!,
            subsManager: subscriptionsManagerMock?.Object!,
            queueName: queueName,
            retryCount: retryCount);

        rabbitMqConnectionMock?.Object.TryConnect();

        TestIntegrationEvent? testIntegrationEvent = default;

        IntegrationEventFixture integrationEventFixture = new(queueName);

        var eventName = integrationEventFixture.GetType().Name;

        var body = JsonSerializer.SerializeToUtf8Bytes(eventName, eventName.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });

        var properties = rabbitMQClientMock?.Object.CreateBasicProperties();

        var policy = RetryPolicy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                this.loggerMock?.Object.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", integrationEventFixture.Id, $"{time.TotalSeconds:n1}", ex.Message);
            });

        rabbitMQClientMock?.Setup(x => x.BasicPublish(RabbitMQConstants.BrokerName, eventName, true, properties, body))
            .Callback<string, string, bool, IBasicProperties, ReadOnlyMemory<byte>>((exchange, routingKey, mandatory, basicProperties, body) =>
            {
                testIntegrationEvent = new(exchange, routingKey, mandatory, basicProperties, body);
            });

        policy.Execute(() =>
        {
            rabbitMQClientMock?.Object.BasicPublish(RabbitMQConstants.BrokerName, eventName, true, properties, body);
        });

        // ACT
        eventBusRabbitMQSut?.Publish(integrationEventFixture);

        // ASSERT
        rabbitMqConnectionMock?.Verify(x => x.IsConnected, Times.AtLeast(1));
        rabbitMqConnectionMock?.Verify(x => x.CreateModel(), Times.AtLeast(1));
        rabbitMQClientMock?.Verify(x => x.BasicPublish(RabbitMQConstants.BrokerName, eventName, true, properties, body), times: Times.AtLeast(1));
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_SubscribeDynamic_Subscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_SubscribeDynamic_Subscribe_Event_Successfully()
    {
        // ARRANGE 
        rabbitMqConnectionMock?.Setup(x => x.IsConnected).Returns(true);
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);

        subscriptionsManagerMock?.Setup(x => x.AddDynamicSubscription<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture)));

        eventBusRabbitMQSut = new(
           persistentConnection: rabbitMqConnectionMock?.Object!,
           logger: loggerMock?.Object!,
           autofac: autofacMock?.Object!,
           subsManager: subscriptionsManagerMock?.Object!,
           queueName: queueName,
           retryCount: retryCount);

        // ACT
        eventBusRabbitMQSut?.SubscribeDynamic<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture));

        // ASSERT
        rabbitMqConnectionMock?.Verify(x => x.IsConnected, Times.AtLeast(1));
        subscriptionsManagerMock?.Verify(x => x.AddDynamicSubscription<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture)), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_Subscribe_Then_Subscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_Subscribe_Then_Subscribe_Event_Successfully()
    {
        // ARRANGE 
        rabbitMqConnectionMock?.Setup(x => x.IsConnected).Returns(true);
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);

        eventBusRabbitMQSut = new(
           persistentConnection: rabbitMqConnectionMock?.Object!,
           logger: loggerMock?.Object!,
           autofac: autofacMock?.Object!,
           subsManager: subscriptionsManagerMock?.Object!,
           queueName: queueName,
           retryCount: retryCount);

        // ACT
        eventBusRabbitMQSut?.Subscribe<IntegrationEventFixture, IntegrationEventHandlerFixture>();

        // ASSERT
        rabbitMqConnectionMock?.Verify(x => x.IsConnected, Times.AtLeast(1));
        subscriptionsManagerMock?.Verify(x => x.AddSubscription<IntegrationEventFixture, IntegrationEventHandlerFixture>(), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_Unsubscribe_Then_Unubscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_Unsubscribe_Then_Unubscribe_Event_Successfully()
    {
        // ARRANGE 
        rabbitMqConnectionMock?.Setup(x => x.IsConnected).Returns(true);
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);

        eventBusRabbitMQSut = new(
           persistentConnection: rabbitMqConnectionMock?.Object!,
           logger: loggerMock?.Object!,
           autofac: autofacMock?.Object!,
           subsManager: subscriptionsManagerMock?.Object!,
           queueName: queueName,
           retryCount: retryCount);

        // ACT
        eventBusRabbitMQSut?.Unsubscribe<IntegrationEventFixture, IntegrationEventHandlerFixture>();

        // ASSERT
        rabbitMqConnectionMock?.Verify(x => x.IsConnected, Times.AtLeast(1));
        subscriptionsManagerMock?.Verify(x => x.RemoveSubscription<IntegrationEventFixture, IntegrationEventHandlerFixture>(), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_Unsubscribe_Then_Unubscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_UnsubscribeDynamic_Then_Unubscribe_Event_Successfully()
    {
        // ARRANGE 
        rabbitMqConnectionMock?.Setup(x => x.IsConnected).Returns(true);
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);

        eventBusRabbitMQSut = new(
           persistentConnection: rabbitMqConnectionMock?.Object!,
           logger: loggerMock?.Object!,
           autofac: autofacMock?.Object!,
           subsManager: subscriptionsManagerMock?.Object!,
           queueName: queueName,
           retryCount: retryCount);

        // ACT
        eventBusRabbitMQSut?.UnsubscribeDynamic<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture));

        // ASSERT
        rabbitMqConnectionMock?.Verify(x => x.IsConnected, Times.AtLeast(1));
        subscriptionsManagerMock?.Verify(x => x.RemoveDynamicSubscription<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture)), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_Unsubscribe_Then_Unubscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_Dispose_Then_Dispose_Successfully()
    {
        // ARRANGE 
        rabbitMqConnectionMock?.Setup(x => x.IsConnected).Returns(true);
        rabbitMqConnectionMock?.Setup(x => x.CreateModel()).Returns(rabbitMQClientMock?.Object!);

        rabbitMQClientMock?.Setup(x => x.CreateBasicProperties()).Returns(basicPropertiesMock?.Object!);

        eventBusRabbitMQSut = new(
           persistentConnection: rabbitMqConnectionMock?.Object!,
           logger: loggerMock?.Object!,
           autofac: autofacMock?.Object!,
           subsManager: subscriptionsManagerMock?.Object!,
           queueName: queueName,
           retryCount: retryCount);

        // ACT
        eventBusRabbitMQSut?.Dispose();

        // ASSERT
        rabbitMqConnectionMock?.Verify(x => x.IsConnected, Times.AtLeast(1));
        rabbitMQClientMock?.Verify(x => x.Dispose(), times: Times.Once);
    }

    /// <summary>
    /// TestClean
    /// </summary>
    [TestCleanup]
    public void TestClean()
    {
        rabbitMqConnectionMock = null!;
        loggerMock = null!;
        subscriptionsManagerMock = null!;
        rabbitMQClientMock = null!;
        autofacMock = null!;
        connectionMock = null!;
        basicPropertiesMock = null!;
        eventBusRabbitMQSut = null!;
        connectionFactoryMock = null!;
    }
}