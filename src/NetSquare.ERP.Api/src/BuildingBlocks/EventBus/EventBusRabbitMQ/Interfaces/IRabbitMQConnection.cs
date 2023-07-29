//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBusRabbitMQ;

/// <summary>
/// Defines the <see cref="IRabbitMQConnection" />.
/// </summary>
public interface IRabbitMQConnection : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether IsConnected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// The TryConnect.
    /// </summary>
    /// <returns>The <see cref="bool"/>.</returns>
    bool TryConnect();

    /// <summary>
    /// The CreateModel.
    /// </summary>
    /// <returns>The <see cref="IModel"/>.</returns>
    IModel CreateModel();
}