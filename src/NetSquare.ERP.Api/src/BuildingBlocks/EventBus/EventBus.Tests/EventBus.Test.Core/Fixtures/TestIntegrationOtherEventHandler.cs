//-----------------------------------------------------------------------
// <copyright file="TestIntegrationOtherEventHandler.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace EventBus.Test.Core.Fixtures;

/// <summary>
/// Defines the <see cref="TestIntegrationOtherEventHandler" />.
/// </summary>
public class TestIntegrationOtherEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
{
    /// <summary>
    /// The Handled
    /// </summary>
    public bool Handled { get; private set; }

    /// <summary>
    /// Initialize the TestIntegrationOtherEventHandler
    /// </summary>
    public TestIntegrationOtherEventHandler()
    {
        Handled = false;
    }

    /// <summary>
    /// The Handle
    /// </summary>
    /// <param name="event"><see cref="TestIntegrationEvent"/></param>
    /// <returns></returns>
    public async Task Handle(TestIntegrationEvent @event)
    {
        Handled = await Task.FromResult(true);
    }
}