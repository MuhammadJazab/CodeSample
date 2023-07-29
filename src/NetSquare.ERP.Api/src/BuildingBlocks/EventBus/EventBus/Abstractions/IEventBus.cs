//-----------------------------------------------------------------------
// <copyright file="IEventBus.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus.Abstractions;
/// <summary>
/// Defines the <see cref="IEventBus" />.
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// The Publish.
    /// </summary>
    /// <param name="@event">The event<see cref="IntegrationEvent"/>.</param>
    void Publish(IntegrationEvent @event);

    /// <summary>
    /// The Subscribe.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    void Subscribe<T, TH>()
    where T : IntegrationEvent
    where TH : IIntegrationEventHandler<T>;

    /// <summary>
    /// The SubscribeDynamic.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    void SubscribeDynamic<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler;

    /// <summary>
    /// The UnsubscribeDynamic.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    void UnsubscribeDynamic<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler;

    /// <summary>
    /// The Unsubscribe.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    void Unsubscribe<T, TH>()
    where TH : IIntegrationEventHandler<T>
    where T : IntegrationEvent;
}

