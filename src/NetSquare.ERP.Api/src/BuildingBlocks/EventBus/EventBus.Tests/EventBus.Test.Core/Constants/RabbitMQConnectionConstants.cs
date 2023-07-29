//-----------------------------------------------------------------------
// <copyright file="RabbitMQConnectionConstants.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBus.Test.Core.Constants;

/// <summary>
/// Defines the <see cref="RabbitMQConnectionTests " />.
/// </summary>
public static class RabbitMQConnectionConstants
{
    /// <summary>
    /// Get the Host name for test
    /// </summary>
    public const string Host = "amqp://localhost";

    public const string CreateModelException = "No RabbitMQ connections are available to perform this action";
}