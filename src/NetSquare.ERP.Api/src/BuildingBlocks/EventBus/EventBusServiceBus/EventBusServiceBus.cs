//-----------------------------------------------------------------------
// <copyright file="EventBusServiceBus.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBusServiceBus;

/// <summary>
/// Defines the <see cref="EventBusServiceBus" />.
/// </summary>
public class EventBusServiceBus : IEventBus, IAsyncDisposable
{
    /// <summary>
    /// Defines the serviceBusPersisterConnection.
    /// </summary>
    private readonly IServiceBusPersisterConnection serviceBusPersisterConnection;

    /// <summary>
    /// Defines the logger.
    /// </summary>
    private readonly ILogger<EventBusServiceBus> logger;

    /// <summary>
    /// Defines the subsManager.
    /// </summary>
    private readonly IEventBusSubscriptionsManager subsManager;

    /// <summary>
    /// Defines the autofac.
    /// </summary>
    private readonly ILifetimeScope autofac;

    /// <summary>
    /// Defines the subscriptionName.
    /// </summary>
    private readonly string subscriptionName;

    /// <summary>
    /// Defines the sender.
    /// </summary>
    private readonly ServiceBusSender sender;

    /// <summary>
    /// Defines the processor.
    /// </summary>
    private readonly ServiceBusProcessor processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventBusServiceBus"/> class.
    /// </summary>
    /// <param name="serviceBusPersisterConnection">The serviceBusPersisterConnection<see cref="IServiceBusPersisterConnection"/>.</param>
    /// <param name="logger">The logger<see cref="ILogger{EventBusServiceBus}"/>.</param>
    /// <param name="subsManager">The subsManager<see cref="IEventBusSubscriptionsManager"/>.</param>
    /// <param name="autofac">The autofac<see cref="ILifetimeScope"/>.</param>
    /// <param name="subscriptionClientName">The subscriptionClientName<see cref="string"/>.</param>
    public EventBusServiceBus(IServiceBusPersisterConnection serviceBusPersisterConnection,
    ILogger<EventBusServiceBus> logger, IEventBusSubscriptionsManager subsManager, ILifetimeScope autofac, string subscriptionClientName)
    {
        this.serviceBusPersisterConnection = serviceBusPersisterConnection;
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
        this.autofac = autofac;
        this.subscriptionName = subscriptionClientName;
        this.sender = this.serviceBusPersisterConnection.TopicClient.CreateSender(EventBusServiceConstants.TopicName);
        ServiceBusProcessorOptions options = new() { MaxConcurrentCalls = 10, AutoCompleteMessages = false };
        this.processor = this.serviceBusPersisterConnection.TopicClient.CreateProcessor(EventBusServiceConstants.TopicName, subscriptionName, options);

        RemoveDefaultRule();
        RegisterSubscriptionClientMessageHandlerAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// The Publish.
    /// </summary>
    /// <param name="@event">The event<see cref="IntegrationEvent"/>.</param>
    public void Publish(IntegrationEvent @event)
    {
        var eventName = @event.GetType().Name.Replace(EventBusServiceConstants.IntegrationEvent, "");
        var jsonMessage = JsonSerializer.Serialize(@event, @event.GetType());
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        var message = new ServiceBusMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            Body = new BinaryData(body),
            Subject = eventName,
        };

        this.sender.SendMessageAsync(message)
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// The SubscribeDynamic.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    public void SubscribeDynamic<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler
    {
        this.logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

        this.subsManager.AddDynamicSubscription<TH>(eventName);
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
        var eventName = typeof(T).Name.Replace(EventBusServiceConstants.IntegrationEvent, "");

        var containsKey = this.subsManager.HasSubscriptionsForEvent<T>();
        if (!containsKey)
        {
            try
            {
                serviceBusPersisterConnection.AdministrationClient.CreateRuleAsync(EventBusServiceConstants.TopicName, this.subscriptionName, new CreateRuleOptions
                {
                    Filter = new CorrelationRuleFilter() { Subject = eventName },
                    Name = eventName
                }).GetAwaiter().GetResult();
            }
            catch (ServiceBusException)
            {
                this.logger.LogWarning("The messaging entity {eventName} already exists.", eventName);
            }
        }

        this.logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

        this.subsManager.AddSubscription<T, TH>();
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
        var eventName = typeof(T).Name.Replace(EventBusServiceConstants.IntegrationEvent, "");

        try
        {
            serviceBusPersisterConnection
                .AdministrationClient
                .DeleteRuleAsync(EventBusServiceConstants.TopicName, this.subscriptionName, eventName)
                .GetAwaiter()
                .GetResult();
        }
        catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessagingEntityNotFound)
        {
            this.logger.LogWarning("The messaging entity {eventName} Could not be found.", eventName);
        }

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
        this.logger.LogInformation("Unsubscribing from dynamic event {EventName}", eventName);

        this.subsManager.RemoveDynamicSubscription<TH>(eventName);
    }

    /// <summary>
    /// The RegisterSubscriptionClientMessageHandlerAsync.
    /// </summary>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task RegisterSubscriptionClientMessageHandlerAsync()
    {
        this.processor.ProcessMessageAsync +=
            async (args) =>
            {
                var eventName = $"{args.Message.Subject}{EventBusServiceConstants.IntegrationEvent}";
                string messageData = args.Message.Body.ToString();

                // Complete the message so that it is not received again.
                if (await ProcessEvent(eventName, messageData))
                {
                    await args.CompleteMessageAsync(args.Message);
                }
            };

        this.processor.ProcessErrorAsync += ErrorHandler;
        await this.processor.StartProcessingAsync();
    }

    /// <summary>
    /// The ErrorHandler.
    /// </summary>
    /// <param name="args">The args<see cref="ProcessErrorEventArgs"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        var ex = args.Exception;
        var context = args.ErrorSource;

        this.logger.LogError(ex, "ERROR handling message: {ExceptionMessage} - Context: {@ExceptionContext}", ex.Message, context);

        return Task.CompletedTask;
    }

    /// <summary>
    /// The ProcessEvent.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{bool}"/>.</returns>
    private async Task<bool> ProcessEvent(string eventName, string message)
    {
        if (this.subsManager.HasSubscriptionsForEvent(eventName))
        {
            var scope = this.autofac.BeginLifetimeScope(EventBusServiceConstants.AutoFacScopeName);
            var subscriptions = this.subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                if (subscription.IsDynamic)
                {
                    if (scope.ResolveOptional(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler) continue;

                    using dynamic eventData = JsonDocument.Parse(message);
                    await handler.Handle(eventData);
                }
                else
                {
                    var handler = scope.ResolveOptional(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = this.subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType?.GetMethod("Handle")?.Invoke(handler, new[] { integrationEvent })!;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// The RemoveDefaultRule.
    /// </summary>
    private void RemoveDefaultRule()
    {
        try
        {
            serviceBusPersisterConnection
                .AdministrationClient
                .DeleteRuleAsync(EventBusServiceConstants.TopicName, this.subscriptionName, RuleProperties.DefaultRuleName)
                .GetAwaiter()
                .GetResult();
        }
        catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessagingEntityNotFound)
        {
            this.logger.LogWarning("The messaging entity {DefaultRuleName} Could not be found.", RuleProperties.DefaultRuleName);
        }
    }

    /// <summary>
    /// The DisposeAsync.
    /// </summary>
    /// <returns>The <see cref="ValueTask"/>.</returns>
    public async ValueTask DisposeAsync()
    {
        this.subsManager.Clear();
        await this.processor.CloseAsync();
        GC.SuppressFinalize(this);
    }
}