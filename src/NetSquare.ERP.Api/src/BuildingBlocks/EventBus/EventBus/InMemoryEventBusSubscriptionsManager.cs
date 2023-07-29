//-----------------------------------------------------------------------
// <copyright file="InMemoryEventBusSubscriptionsManager.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus;

/// <summary>
/// Defines the <see cref="InMemoryEventBusSubscriptionsManager" />.
/// </summary>
public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    /// <summary>
    /// Defines the _handlers.
    /// </summary>
    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;

    /// <summary>
    /// Defines the _eventTypes.
    /// </summary>
    private readonly List<Type> _eventTypes;

    /// <summary>
    /// Defines the OnEventRemoved.
    /// </summary>
    public event EventHandler<string> OnEventRemoved;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryEventBusSubscriptionsManager"/> class.
    /// </summary>
    public InMemoryEventBusSubscriptionsManager()
    {
        _handlers = new Dictionary<string, List<SubscriptionInfo>>();
        _eventTypes = new List<Type>();
    }

    /// <summary>
    /// Gets a value indicating whether IsEmpty.
    /// </summary>
    public bool IsEmpty => _handlers is { Count: 0 };

    /// <summary>
    /// The Clear.
    /// </summary>
    public void Clear() => _handlers.Clear();

    /// <summary>
    /// The AddDynamicSubscription.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    public void AddDynamicSubscription<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler
    {
        DoAddSubscription(typeof(TH), eventName, isDynamic: true);
    }

    /// <summary>
    /// The AddSubscription.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    public void AddSubscription<T, TH>()
    where T : IntegrationEvent
    where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();

        DoAddSubscription(typeof(TH), eventName, isDynamic: false);

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }
    }

    /// <summary>
    /// The DoAddSubscription.
    /// </summary>
    /// <param name="handlerType">The handlerType<see cref="Type"/>.</param>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <param name="isDynamic">The isDynamic<see cref="bool"/>.</param>
    private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            _handlers.Add(eventName, new List<SubscriptionInfo>());
        }

        if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
        {
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
        }

        if (isDynamic)
        {
            _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
        }
        else
        {
            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
        }
    }

    /// <summary>
    /// The RemoveDynamicSubscription.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    public void RemoveDynamicSubscription<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler
    {
        var handlerToRemove = FindDynamicSubscriptionToRemove<TH>(eventName);
        DoRemoveHandler(eventName, handlerToRemove);
    }

    /// <summary>
    /// The RemoveSubscription.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    public void RemoveSubscription<T, TH>()
    where TH : IIntegrationEventHandler<T>
    where T : IntegrationEvent
    {
        var handlerToRemove = FindSubscriptionToRemove<T, TH>();
        var eventName = GetEventKey<T>();
        DoRemoveHandler(eventName, handlerToRemove);
    }

    /// <summary>
    /// The DoRemoveHandler.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <param name="subsToRemove">The subsToRemove<see cref="SubscriptionInfo"/>.</param>
    private void DoRemoveHandler(string eventName, SubscriptionInfo subsToRemove)
    {
        if (subsToRemove != null)
        {
            _handlers[eventName].Remove(subsToRemove);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
                var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                if (eventType != null)
                {
                    _eventTypes.Remove(eventType);
                }
                RaiseOnEventRemoved(eventName);
            }

        }
    }

    /// <summary>
    /// The GetHandlersForEvent.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="IEnumerable{SubscriptionInfo}"/>.</returns>
    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return GetHandlersForEvent(key);
    }

    /// <summary>
    /// The GetHandlersForEvent.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <returns>The <see cref="IEnumerable{SubscriptionInfo}"/>.</returns>
    public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

    /// <summary>
    /// The RaiseOnEventRemoved.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    private void RaiseOnEventRemoved(string eventName)
    {
        var handler = OnEventRemoved;
        handler?.Invoke(this, eventName);
    }

    /// <summary>
    /// The FindDynamicSubscriptionToRemove.
    /// </summary>
    /// <typeparam name="TH">.</typeparam>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <returns>The <see cref="SubscriptionInfo"/>.</returns>
    private SubscriptionInfo FindDynamicSubscriptionToRemove<TH>(string eventName)
    where TH : IDynamicIntegrationEventHandler
    {
        return DoFindSubscriptionToRemove(eventName, typeof(TH));
    }

    /// <summary>
    /// The FindSubscriptionToRemove.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <typeparam name="TH">.</typeparam>
    /// <returns>The <see cref="SubscriptionInfo"/>.</returns>
    private SubscriptionInfo FindSubscriptionToRemove<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();
        return DoFindSubscriptionToRemove(eventName, typeof(TH));
    }

    /// <summary>
    /// The DoFindSubscriptionToRemove.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <param name="handlerType">The handlerType<see cref="Type"/>.</param>
    /// <returns>The <see cref="SubscriptionInfo"/>.</returns>
    private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            return null!;
        }

        return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType)!;
    }

    /// <summary>
    /// The HasSubscriptionsForEvent.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return HasSubscriptionsForEvent(key);
    }

    /// <summary>
    /// The HasSubscriptionsForEvent.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

    /// <summary>
    /// The GetEventTypeByName.
    /// </summary>
    /// <param name="eventName">The eventName<see cref="string"/>.</param>
    /// <returns>The <see cref="Type"/>.</returns>
    public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName)!;

    /// <summary>
    /// The GetEventKey.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="string"/>.</returns>
    public string GetEventKey<T>()
    {
        return typeof(T).Name;
    }
}