//-----------------------------------------------------------------------
// <copyright file="IDynamicIntegrationEventHandler.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus.Abstractions;
/// <summary>
/// Defines the <see cref="IDynamicIntegrationEventHandler" />.
/// </summary>
public interface IDynamicIntegrationEventHandler
{
    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="eventData">The eventData<see cref="dynamic"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task Handle(dynamic eventData);
}