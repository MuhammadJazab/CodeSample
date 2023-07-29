//-----------------------------------------------------------------------
// <copyright file="InMemorySubscriptionManagerTests.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBus.Tests;

/// <summary>
/// Defines the <see cref="InMemorySubscriptionManagerTests" />.
/// </summary>
[TestClass]
public class InMemorySubscriptionManagerTests
{
    /// <summary>
    /// After_Creation_Should_Be_Empty
    /// </summary>
    [TestMethod]
    public void After_Creation_Should_Be_Empty()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        Assert.IsTrue(manager.IsEmpty);
    }

    /// <summary>
    /// After_One_Event_Subscription_Should_Contain_The_Event
    /// </summary>
    [TestMethod]
    public void After_One_Event_Subscription_Should_Contain_The_Event()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        Assert.IsTrue(manager.HasSubscriptionsForEvent<TestIntegrationEvent>());
    }

    /// <summary>
    /// After_All_Subscriptions_Are_Deleted_Event_Should_No_Longer_Exists
    /// </summary>
    [TestMethod]
    public void After_All_Subscriptions_Are_Deleted_Event_Should_No_Longer_Exists()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        Assert.IsFalse(manager.HasSubscriptionsForEvent<TestIntegrationEvent>());
    }

    /// <summary>
    /// Deleting_Last_Subscription_Should_Raise_On_Deleted_Event
    /// </summary>
    [TestMethod]
    public void Deleting_Last_Subscription_Should_Raise_On_Deleted_Event()
    {
        bool raised = false;
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.OnEventRemoved += (o, e) => raised = true;
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        Assert.IsTrue(raised);
    }

    /// <summary>
    /// Get_Handlers_For_Event_Should_Return_All_Handlers
    /// </summary>
    [TestMethod]
    public void Get_Handlers_For_Event_Should_Return_All_Handlers()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationOtherEventHandler>();
        var handlers = manager.GetHandlersForEvent<TestIntegrationEvent>();
        Assert.AreEqual(2, handlers.Count());
    }
}