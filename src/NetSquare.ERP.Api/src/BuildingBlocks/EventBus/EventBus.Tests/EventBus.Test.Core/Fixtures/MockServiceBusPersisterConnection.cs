//-----------------------------------------------------------------------
// <copyright file="MockServiceBusPersisterConnection.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBus.Test.Core.Fixtures;

/// <summary>
/// Defines the <see cref="MockServiceBusPersisterConnection " />.
/// </summary>
public class MockServiceBusPersisterConnection : IServiceBusPersisterConnection
{
    /// <summary>
    /// TopicClient
    /// </summary>
    public ServiceBusClient TopicClient { get; }

    /// <summary>
    /// AdministrationClient
    /// </summary>
    public ServiceBusAdministrationClient AdministrationClient { get; }

    /// <summary>
    /// Initialize the MockServiceBusPersisterConnection
    /// </summary>
    /// <param name="topicClient"><see cref="ServiceBusClient"/></param>
    /// <param name="administrationClient"><see cref="ServiceBusAdministrationClient"/></param>
    public MockServiceBusPersisterConnection(ServiceBusClient topicClient, ServiceBusAdministrationClient administrationClient)
    {
        TopicClient = topicClient;
        AdministrationClient = administrationClient;
    }

    public async ValueTask DisposeAsync()
    {
        await Task.CompletedTask;
    }
}