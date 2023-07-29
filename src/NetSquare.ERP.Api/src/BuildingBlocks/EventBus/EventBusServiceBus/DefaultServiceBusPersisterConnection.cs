//-----------------------------------------------------------------------
// <copyright file="DefaultServiceBusPersisterConnection.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBusServiceBus;

/// <summary>
/// Defines the <see cref="DefaultServiceBusPersisterConnection" />.
/// </summary>
public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
{
    /// <summary>
    /// Defines the _serviceBusConnectionString.
    /// </summary>
    private readonly string _serviceBusConnectionString;

    /// <summary>
    /// Defines the _topicClient.
    /// </summary>
    private ServiceBusClient _topicClient;

    /// <summary>
    /// Defines the _subscriptionClient.
    /// </summary>
    private readonly ServiceBusAdministrationClient _subscriptionClient;

    public bool Disposed { get; set; }



    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultServiceBusPersisterConnection"/> class.
    /// </summary>
    /// <param name="serviceBusConnectionString">The serviceBusConnectionString<see cref="string"/>.</param>
    public DefaultServiceBusPersisterConnection(string serviceBusConnectionString)
    {
        _serviceBusConnectionString = serviceBusConnectionString;
        _subscriptionClient = new ServiceBusAdministrationClient(_serviceBusConnectionString);
        _topicClient = new ServiceBusClient(_serviceBusConnectionString);
    }

    /// <summary>
    /// Gets the TopicClient.
    /// </summary>
    public ServiceBusClient TopicClient
    {
        get
        {
            if (_topicClient.IsClosed)
            {
                _topicClient = new ServiceBusClient(_serviceBusConnectionString);
            }
            return _topicClient;
        }
    }

    /// <summary>
    /// Gets the AdministrationClient.
    /// </summary>
    public ServiceBusAdministrationClient AdministrationClient =>
    _subscriptionClient;

    /// <summary>
    /// The CreateModel.
    /// </summary>
    /// <returns>The <see cref="ServiceBusClient"/>.</returns>
    public ServiceBusClient CreateModel()
    {
        if (_topicClient.IsClosed)
        {
            _topicClient = new ServiceBusClient(_serviceBusConnectionString);
        }

        return _topicClient;
    }

    /// <summary>
    /// The DisposeAsync.
    /// </summary>
    /// <returns>The <see cref="ValueTask"/>.</returns>
    public async ValueTask DisposeAsync()
    {
        if (Disposed) return;

        Disposed = true;
        await _topicClient.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}