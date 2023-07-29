//-----------------------------------------------------------------------
// <copyright file="EventBusRabbitMQ.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBusRabbitMQ;

/// <summary>
/// Defines the <see cref="EventBusRabbitMQ" />.
/// </summary>
public sealed class EventBusRabbitMQ : IEventBus, IDisposable
{
    /// <summary>
    /// Defines the this.rabbitMqConnection.
    /// </summary>
    private readonly IRabbitMQConnection rabbitMqConnection;

    /// <summary>
    /// Defines the logger.
    /// </summary>
    private readonly ILogger<EventBusRabbitMQ> logger;

    /// <summary>
    /// Defines the subsManager.
    /// </summary>
    private readonly IEventBusSubscriptionsManager subsManager;

    /// <summary>
    /// Defines the autofac.
    /// </summary>
    private readonly ILifetimeScope autofac;

    /// <summary>
    /// Defines the retryCount.
    /// </summary>
    private readonly int retryCount;

    /// <summary>
    /// Defines the consumerChannel.
    /// </summary>
    private IModel consumerChannel;

    /// <summary>
    /// Defines the queueName.
    /// </summary>
    private string queueName;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventBusRabbitMQ"/> class.
    /// </summary>
    /// <param name="persistentConnection">The persistentConnection<see cref="IRabbitMQConnection"/>.</param>
    /// <param name="logger">The logger<see cref="ILogger{EventBusRabbitMQ}"/>.</param>
    /// <param name="autofac">The autofac<see cref="ILifetimeScope"/>.</param>
    /// <param name="subsManager">The subsManager<see cref="IEventBusSubscriptionsManager"/>.</param>
    /// <param name="queueName">The queueName<see cref="string"/>.</param>
    /// <param name="retryCount">The retryCount<see cref="int"/>.</param>
    public EventBusRabbitMQ(IRabbitMQConnection persistentConnection, ILogger<EventBusRabbitMQ> logger,
    ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager, string queueName = null!, int retryCount = 5)
    {
        this.rabbitMqConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
        this.queueName = queueName;
        this.consumerChannel = CreateConsumerChannel();
        this.autofac = autofac;
        this.retryCount = retryCount;
        this.subsManager.OnEventRemoved += SubsManager_OnEventRemoved!;
    }

    /// <summary>
    /// The SubsManager_OnEventRemoved.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    private void SubsManager_OnEventRemoved(object sender, string eventName)
    {
        if (!this.rabbitMqConnection.IsConnected)
        {
            this.rabbitMqConnection.TryConnect();
        }

        using var channel = this.rabbitMqConnection.CreateModel();
        channel.QueueUnbind(queue: queueName,
            exchange: RabbitMQConstants.BrokerName,
            routingKey: eventName);

        if (subsManager.IsEmpty)
        {
            queueName = string.Empty;
            consumerChannel.Close();
        }
    }

    /// <summary>
    /// The Publish.
    /// </summary>
    /// <param name="@event">The event<see cref="IntegrationEvent"/>.</param>
    public void Publish(IntegrationEvent @event)
    {
        if (!this.rabbitMqConnection.IsConnected)
        {
            this.rabbitMqConnection.TryConnect();
        }

        var policy = RetryPolicy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                this.logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
            });

        var eventName = @event.GetType().Name;

        this.logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

        using var channel = this.rabbitMqConnection.CreateModel();
        this.logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        channel.ExchangeDeclare(exchange: RabbitMQConstants.BrokerName, type: ExchangeType.Fanout);

        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // persistent

            logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

            channel.BasicPublish(
                exchange: RabbitMQConstants.BrokerName,
                routingKey: eventName,
                mandatory: true,
                basicProperties: properties,
                body: body);
        });
    }

    /// <summary>
    /// The SubscribeDynamic.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    public void SubscribeDynamic<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler
    {
        this.logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        DoInternalSubscription(eventName);
        this.subsManager.AddDynamicSubscription<TH>(eventName);
        StartBasicConsume();
    }

    /// <summary>
    /// The Subscribe.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    public void Subscribe<T, TH>()
    where T : IntegrationEvent
    where TH : IIntegrationEventHandler<T>
    {
        var eventName = this.subsManager.GetEventKey<T>();
        DoInternalSubscription(eventName);

        this.logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        this.subsManager.AddSubscription<T, TH>();
        StartBasicConsume();
    }

    /// <summary>
    /// The DoInternalSubscription.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    private void DoInternalSubscription(string eventName)
    {
        var containsKey = this.subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!this.rabbitMqConnection.IsConnected)
            {
                this.rabbitMqConnection.TryConnect();
            }

            this.consumerChannel.QueueBind(queue: queueName, exchange: RabbitMQConstants.BrokerName, routingKey: eventName);
        }
    }

    /// <summary>
    /// The Unsubscribe.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    public void Unsubscribe<T, TH>()
    where T : IntegrationEvent
    where TH : IIntegrationEventHandler<T>
    {
        var eventName = subsManager.GetEventKey<T>();

        this.logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        this.subsManager.RemoveSubscription<T, TH>();
    }

    /// <summary>
    /// The UnsubscribeDynamic.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    public void UnsubscribeDynamic<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler
    {
        this.subsManager.RemoveDynamicSubscription<TH>(eventName);
    }

    /// <summary>
    /// The Dispose.
    /// </summary>
    public void Dispose()
    {
        consumerChannel?.Dispose();

        this.subsManager.Clear();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// The StartBasicConsume.
    /// </summary>
    private void StartBasicConsume()
    {
        this.logger.LogTrace("Starting RabbitMQ basic consume");

        if (this.consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(this.consumerChannel);

            consumer.Received += Consumer_Received;

            this.consumerChannel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: consumer);
        }
        else
        {
            this.logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
        }
    }

    /// <summary>
    /// The Consumer_Received.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="eventArgs">The eventArgs<see cref="BasicDeliverEventArgs"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            if (message.ToLowerInvariant().Contains("throw-fake-exception"))
            {
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
            }

            await ProcessEvent(eventName, message);
        }
        catch (Exception ex)
        {
            this.logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
        }

        // Even on exception we take the message off the queue.
        // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
        // For more information see: https://www.rabbitmq.com/dlx.html
        this.consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }

    /// <summary>
    /// The CreateConsumerChannel.
    /// </summary>
    /// <returns>The <see cref="IModel"/>.</returns>
    private IModel CreateConsumerChannel()
    {
        if (!this.rabbitMqConnection.IsConnected)
        {
            this.rabbitMqConnection.TryConnect();
        }

        this.logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = this.rabbitMqConnection.CreateModel();

        channel.ExchangeDeclare(exchange: RabbitMQConstants.BrokerName, type: ExchangeType.Fanout);

        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        channel.CallbackException += (sender, ea) =>
        {
            this.logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            this.consumerChannel.Dispose();
            this.consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };

        return channel;
    }

    /// <summary>
    /// The ProcessEvent.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task ProcessEvent(string eventName, string message)
    {
        this.logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        if (this.subsManager.HasSubscriptionsForEvent(eventName))
        {
            await using var scope = this.autofac.BeginLifetimeScope(RabbitMQConstants.AutoFacScopeName);
            var subscriptions = this.subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                if (subscription.IsDynamic)
                {
                    if (scope.ResolveOptional(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler) continue;
                    using dynamic eventData = JsonDocument.Parse(message);
                    await Task.Yield();
                    await handler.Handle(eventData);
                }
                else
                {
                    var handler = scope.ResolveOptional(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                }
            }
        }
        else
        {
            this.logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }
}