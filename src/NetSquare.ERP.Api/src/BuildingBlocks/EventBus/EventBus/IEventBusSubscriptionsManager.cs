//-----------------------------------------------------------------------
// <copyright file="IEventBusSubscriptionsManager.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus;

/// <summary>
/// Defines the <see cref="IEventBusSubscriptionsManager" />.
/// </summary>
public interface IEventBusSubscriptionsManager
{
    /// <summary>
    /// Gets a value indicating whether IsEmpty.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Defines the OnEventRemoved.
    /// </summary>
    event EventHandler<string> OnEventRemoved;

    /// <summary>
    /// The AddDynamicSubscription.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    void AddDynamicSubscription<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler;

    /// <summary>
    /// The AddSubscription.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    void AddSubscription<T, TH>()
    where T : IntegrationEvent
    where TH : IIntegrationEventHandler<T>;

    /// <summary>
    /// The RemoveSubscription.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    void RemoveSubscription<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;

    /// <summary>
    /// The RemoveDynamicSubscription.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    void RemoveDynamicSubscription<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler;

    /// <summary>
    /// The HasSubscriptionsForEvent.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="bool"/>.</returns>
    bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

    /// <summary>
    /// The HasSubscriptionsForEvent.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    bool HasSubscriptionsForEvent(string eventName);

    /// <summary>
    /// The GetEventTypeByName.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <returns>The <see cref="Type"/>.</returns>
    Type GetEventTypeByName(string eventName);

    /// <summary>
    /// The Clear.
    /// </summary>
    void Clear();

    /// <summary>
    /// The GetHandlersForEvent.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="IEnumerable{SubscriptionInfo}"/>.</returns>
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;

    /// <summary>
    /// The GetHandlersForEvent.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <returns>The <see cref="IEnumerable{SubscriptionInfo}"/>.</returns>
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

    /// <summary>
    /// The GetEventKey.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="string"/>.</returns>
    string GetEventKey<T>();
}