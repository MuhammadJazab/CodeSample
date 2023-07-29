//-----------------------------------------------------------------------
// <copyright file="IServiceBusPersisterConnection.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBusServiceBus;

/// <summary>
/// Defines the <see cref="IServiceBusPersisterConnection" />.
/// </summary>
public interface IServiceBusPersisterConnection : IAsyncDisposable
{
    /// <summary>
    /// Gets the TopicClient.
    /// </summary>
    ServiceBusClient TopicClient { get; }

    /// <summary>
    /// Gets the AdministrationClient.
    /// </summary>
    ServiceBusAdministrationClient AdministrationClient { get; }
}