//-----------------------------------------------------------------------
// <copyright file="IIntegrationEventLogService.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.IntegrationEventLogEF.Services;

/// <summary>
/// Defines the <see cref="IIntegrationEventLogService" />.
/// </summary>
public interface IIntegrationEventLogService
{
    /// <summary>
    /// The RetrieveEventLogsPendingToPublishAsync.
    /// </summary>
    /// <param name="transactionId">The transactionId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task{IEnumerable{IntegrationEventLogEntry}}"/>.</returns>
    Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);

    /// <summary>
    /// The SaveEventAsync.
    /// </summary>
    /// <param name="@event">The event<see cref="IntegrationEvent"/>.</param>
    /// <param name="transaction">The transaction<see cref="IDbContextTransaction"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);

    /// <summary>
    /// The MarkEventAsPublishedAsync.
    /// </summary>
    /// <param name="eventId">The eventId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task MarkEventAsPublishedAsync(Guid eventId);

    /// <summary>
    /// The MarkEventAsInProgressAsync.
    /// </summary>
    /// <param name="eventId">The eventId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task MarkEventAsInProgressAsync(Guid eventId);

    /// <summary>
    /// The MarkEventAsFailedAsync.
    /// </summary>
    /// <param name="eventId">The eventId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task MarkEventAsFailedAsync(Guid eventId);
}