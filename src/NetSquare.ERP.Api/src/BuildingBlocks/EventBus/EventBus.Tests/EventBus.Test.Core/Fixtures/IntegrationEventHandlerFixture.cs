//-----------------------------------------------------------------------
// <copyright file="IntegrationEventHandlerFixture.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBus.Test.Core.Fixtures;

/// <summary>
/// Defines the <see cref="IntegrationEventHandlerFixture" />.
/// </summary>
public class IntegrationEventHandlerFixture : IIntegrationEventHandler<IntegrationEventFixture>
{
    /// <summary>
    /// The Handler
    /// </summary>
    /// <param name="event"><see cref="IntegrationEventFixture"/></param>
    /// <returns><see cref="Task"/></returns>
    public Task Handle(IntegrationEventFixture @event)
    {
        return Task.CompletedTask;
    }
}