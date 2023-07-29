//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBusRabbitMQ.Constants;

//// <summary>
/// Defines the <see cref="RabbitMQConstants" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class RabbitMQConstants
{
    /// <summary>
    /// Defines the BrokerName.
    /// </summary>
    public const string BrokerName = "CRMT_Event_Bus";

    /// <summary>
    /// Defines the AutoFacScopeName.
    /// </summary>
    public const string AutoFacScopeName = "CRMT_Event_Bus";

    /// <summary>
    /// Defines the IntegrationEvent.
    /// </summary>
    public const string IntegrationEvent = "CRMT_Integration_Event";

    /// <summary>
    /// Defines the SubscriptionClientName.
    /// </summary>
    public const string SubscriptionClientName = "SubscriptionClientName";

    /// <summary>
    /// Defines the EventBusRetryCount.
    /// </summary>
    public const string EventBusRetryCount = "EventBusRetryCount";

    /// <summary>
    /// Defines the EventBusConnection.
    /// </summary>
    public const string EventBusConnection = "EventBusConnection";

    /// <summary>
    /// Defines the EventBusDefaultRetryCount.
    /// </summary>
    public const string EventBusDefaultRetryCount = "EventBusDefaultRetryCount";
}