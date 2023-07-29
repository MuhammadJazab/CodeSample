//-----------------------------------------------------------------------
// <copyright file="InMemoryEventBusSubscriptionsManagerTests.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBus.Tests;

/// <summary>
/// Defines the <see cref="InMemoryEventBusSubscriptionsManagerTests" />.
/// </summary>
[TestClass]
public class InMemoryEventBusSubscriptionsManagerTests
{
    /// <summary>
    /// the handlersMock
    /// </summary>
    private Mock<Dictionary<string, List<SubscriptionInfo>>>? handlersMock;

    /// <summary>
    /// the inMemoryEventBusSubscriptionsManagerSut
    /// </summary>
    private InMemoryEventBusSubscriptionsManager? inMemoryEventBusSubscriptionsManagerSut;

    /// <summary>
    /// GroupCapacityControllerTests_Init
    /// </summary>
    [TestInitialize]
    public void InMemoryEventBusSubscriptionsManager_Init()
    {
        handlersMock = new();
        inMemoryEventBusSubscriptionsManagerSut = new();
    }

    /// <summary>
    /// Given_Constructor_When_Initialized_Then_Create_InMemoryEventBusSubscriptionsManager_Object
    /// </summary>
    [TestMethod]
    public void Given_Constructor_When_Initialized_Then_Create_InMemoryEventBusSubscriptionsManager_Object()
    {
        // ARRANGE

        // ACT

        // ASSERT
        Assert.IsNotNull(inMemoryEventBusSubscriptionsManagerSut);
    }

    /// <summary>
    /// Given_InMemoryEventBusSubscriptionsManager_When_Called_Clear_Then_Clear_Handler
    /// </summary>
    [TestMethod]
    public void Given_InMemoryEventBusSubscriptionsManager_When_Called_Clear_Then_Clear_Handler()
    {
        // ARRANGE

        // ACT
        inMemoryEventBusSubscriptionsManagerSut?.Clear();

        // ASSERT
        Assert.AreEqual(0, handlersMock?.Object.Count);
    }

    /// <summary>
    /// TestClean
    /// </summary>
    [TestCleanup]
    public void TestClean()
    {
        handlersMock = null!;
        inMemoryEventBusSubscriptionsManagerSut = null!;
    }
}