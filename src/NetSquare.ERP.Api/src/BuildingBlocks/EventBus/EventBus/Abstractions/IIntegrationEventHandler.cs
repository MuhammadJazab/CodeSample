//-----------------------------------------------------------------------
// <copyright file="IIntegrationEventHandler.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus.Abstractions;

/// <summary>
/// Defines the <see cref="IIntegrationEventHandler{in TIntegrationEvent}" />.
/// </summary>
/// <typeparam name="TIntegrationEvent">.</typeparam>
public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler where TIntegrationEvent : IntegrationEvent
{
    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="@event">The event<see cref="TIntegrationEvent"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task Handle(TIntegrationEvent @event);
}

/// <summary>
/// Defines the <see cref="IIntegrationEventHandler" />.
/// </summary>
public interface IIntegrationEventHandler
{
}