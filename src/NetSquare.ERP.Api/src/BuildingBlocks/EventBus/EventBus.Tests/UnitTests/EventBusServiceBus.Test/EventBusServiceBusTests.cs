//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBusServiceBus.Test;

/// <summary>
/// Defines the <see cref="EventBusServiceBusTests " />.
/// </summary>
[TestClass]
public class EventBusServiceBusTests
{
    /// <summary>
    /// Defines the serviceBusPersisterConnectionMock
    /// </summary>
    private MockServiceBusPersisterConnection? serviceBusPersisterConnectionMock;

    /// <summary>
    /// Defines the serviceBusAdministrationClientMock
    /// </summary>
    private Mock<ServiceBusAdministrationClient>? serviceBusAdministrationClientMock;

    /// <summary>
    /// Defines the serviceBusClientMock
    /// </summary>
    private Mock<ServiceBusClient>? serviceBusClientMock;

    /// <summary>
    /// Defines the loggerMock.
    /// </summary>
    private Mock<ILogger<NetSquare.ERP.EventBusServiceBus.EventBusServiceBus>>? loggerMock;

    /// <summary>
    /// Defines the subscriptionsManagerMock
    /// </summary>
    private Mock<IEventBusSubscriptionsManager>? subscriptionsManagerMock;

    /// <summary>
    /// Defines the autofacMock
    /// </summary>
    private Mock<ILifetimeScope>? autofacMock;

    /// <summary>
    /// Defines the senderMock.
    /// </summary>
    private Mock<ServiceBusSender>? serviceSenderMock;

    /// <summary>
    /// Defines the serviceBusProcessorMock.
    /// </summary>
    private Mock<ServiceBusProcessor>? serviceBusProcessorMock;

    /// <summary>
    /// Defines the System under test eventBusServiceBus.
    /// </summary>
    private NetSquare.ERP.EventBusServiceBus.EventBusServiceBus? eventBusServiceBusSut;


    /// <summary>
    /// GroupCapacityControllerTests_Init
    /// </summary>
    [TestInitialize]
    public void EventBusServiceBusTests_Init()
    {
        ////ServiceBusProcessorOptions options = new() { MaxConcurrentCalls = 10, AutoCompleteMessages = false };

        serviceSenderMock = new();
        serviceBusClientMock = new();
        serviceBusProcessorMock = new();
        serviceBusAdministrationClientMock = new();

        serviceBusPersisterConnectionMock = new MockServiceBusPersisterConnection(serviceBusClientMock?.Object!, serviceBusAdministrationClientMock?.Object!);

        loggerMock = new();
        autofacMock = new();
        serviceSenderMock = new();
        subscriptionsManagerMock = new();

        serviceBusClientMock?
            .Setup(x => x.CreateSender(It.IsAny<string>()))
            .Returns(serviceSenderMock?.Object!);

        serviceBusClientMock?
            .Setup(x => x.CreateProcessor(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ServiceBusProcessorOptions>()))
            .Returns(serviceBusProcessorMock?.Object!);

        eventBusServiceBusSut = new(
           serviceBusPersisterConnection: serviceBusPersisterConnectionMock,
           logger: loggerMock?.Object!,
           autofac: autofacMock?.Object!,
           subsManager: subscriptionsManagerMock?.Object!,
           subscriptionClientName: EventBusTestConstants.SubscriptionName);
    }

    /// <summary>
    /// Given_Constructor_When_Initialized_Then_Create_EventBusServiceBus_Object
    /// </summary>
    [TestMethod]
    public void Given_Constructor_When_Initialized_Then_Create_EventBusServiceBus_Object()
    {
        // ARRANGE

        // ACT

        // ASSERT
        Assert.IsNotNull(eventBusServiceBusSut);
    }

    /// <summary>
    /// Give_EventBusServiceBus_When_Called_Publish_PublishEvent_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusServiceBus_When_Called_Publish_PublishEvent_Successfully()
    {
        // ARRANGE 
        IntegrationEventFixture integrationEventFixture = new(EventBusTestConstants.IntegrationEvent);

        var eventName = integrationEventFixture.GetType().Name;

        var jsonMessage = JsonSerializer.Serialize(integrationEventFixture, integrationEventFixture.GetType());
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        var message = new ServiceBusMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            Body = new BinaryData(body),
            Subject = eventName,
        };

        serviceSenderMock?
            .Setup(x => x.SendMessageAsync(message, default));

        // ACT
        eventBusServiceBusSut?.Publish(integrationEventFixture);

        // ASSERT
        ////message.Subject.Equals(nameof(IntegrationEventFixture)
        serviceSenderMock?.Verify(x => x.SendMessageAsync(It.Is<ServiceBusMessage>(x => x.Subject == nameof(IntegrationEventFixture)), default), Times.AtLeast(1));
    }

    /// <summary>
    /// Give_EventBusServiceBus_When_Called_SubscribeDynamic_Subscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusServiceBus_When_Called_SubscribeDynamic_Subscribe_Event_Successfully()
    {
        // ARRANGE 

        // ACT
        eventBusServiceBusSut?.SubscribeDynamic<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture));

        // ASSERT
        subscriptionsManagerMock?.Verify(x => x.AddDynamicSubscription<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture)), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusServiceBus_When_Called_Subscribe_Subscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusServiceBus_When_Called_Subscribe_Subscribe_Event_Successfully()
    {
        // ARRANGE 

        // ACT
        eventBusServiceBusSut?.Subscribe<IntegrationEventFixture, IntegrationEventHandlerFixture>();

        // ASSERT
        subscriptionsManagerMock?.Verify(x => x.AddSubscription<IntegrationEventFixture, IntegrationEventHandlerFixture>(), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusServiceBus_When_Called_Unsubscribe_Subscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusServiceBus_When_Called_Unsubscribe_Subscribe_Event_Successfully()
    {
        // ARRANGE 

        // ACT
        eventBusServiceBusSut?.Unsubscribe<IntegrationEventFixture, IntegrationEventHandlerFixture>();

        // ASSERT
        subscriptionsManagerMock?.Verify(x => x.RemoveSubscription<IntegrationEventFixture, IntegrationEventHandlerFixture>(), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusServiceBus_When_Called_UnubscribeDynamic_Subscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusServiceBus_When_Called_UnubscribeDynamic_Subscribe_Event_Successfully()
    {
        // ARRANGE 

        // ACT
        eventBusServiceBusSut?.UnsubscribeDynamic<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture));

        // ASSERT
        subscriptionsManagerMock?.Verify(x => x.RemoveDynamicSubscription<IDynamicIntegrationEventHandler>(nameof(IntegrationEventFixture)), times: Times.Once);
    }

    /// <summary>
    /// Give_EventBusRabbitMQ_When_Called_Unsubscribe_Then_Unubscribe_Event_Successfully
    /// </summary>
    [TestMethod]
    public void Give_EventBusRabbitMQ_When_Called_Dispose_Then_Dispose_Successfully()
    {
        // ARRANGE 

        // ACT
        eventBusServiceBusSut?.DisposeAsync();

        // ASSERT
        subscriptionsManagerMock?.Verify(x => x.Clear(), times: Times.Once);
        serviceBusProcessorMock?.Verify(x => x.CloseAsync(default), times: Times.Once);
    }

    /// <summary>
    /// TestClean
    /// </summary>
    [TestCleanup]
    public void TestClean()
    {
        serviceBusAdministrationClientMock = null!;
        serviceBusClientMock = null!;
        serviceBusPersisterConnectionMock = null!;
        loggerMock = null!;
        subscriptionsManagerMock = null!;
        autofacMock = null!;
        serviceSenderMock = null!;
        serviceBusProcessorMock = null!;
        eventBusServiceBusSut = null!;
    }
}