//-----------------------------------------------------------------------
// <copyright file="EventStateEnum.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.IntegrationEventLogEF;

/// <summary>
/// Defines the EventState.
/// </summary>
public enum EventState
{
    /// <summary>
    /// Defines the NotPublished.
    /// </summary>
    NotPublished = 0,

    /// <summary>
    /// Defines the InProgress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Defines the Published.
    /// </summary>
    Published = 2,

    /// <summary>
    /// Defines the PublishedFailed.
    /// </summary>
    PublishedFailed = 3
}