//-----------------------------------------------------------------------
// <copyright file="IntegrationEventLogService.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.IntegrationEventLogEF.Services;

/// <summary>
/// Defines the <see cref="IntegrationEventLogService" />.
/// </summary>
public class IntegrationEventLogService : IIntegrationEventLogService, IDisposable
{
    /// <summary>
    /// Defines the _integrationEventLogContext.
    /// </summary>
    private readonly IntegrationEventLogContext _integrationEventLogContext;

    /// <summary>
    /// Defines the _eventTypes.
    /// </summary>
    private readonly List<Type> _eventTypes;

    /// <summary>
    /// Defines the _disposedValue.
    /// </summary>
    private volatile bool _disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationEventLogService"/> class.
    /// </summary>
    /// <param name="dbConnection">The dbConnection<see cref="DbConnection"/>.</param>
    public IntegrationEventLogService(DbConnection dbConnection)
    {
        var _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        _integrationEventLogContext = new IntegrationEventLogContext(
            new DbContextOptionsBuilder<IntegrationEventLogContext>()
                .UseSqlServer(_dbConnection)
                .Options);

        _eventTypes = Assembly.Load(Assembly.GetEntryAssembly()?.FullName!)
            .GetTypes()
            .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
            .ToList();
    }

    /// <summary>
    /// The RetrieveEventLogsPendingToPublishAsync.
    /// </summary>
    /// <param name="transactionId">The transactionId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task{IEnumerable{IntegrationEventLogEntry}}"/>.</returns>
    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
    {
        var tid = transactionId.ToString();

        var result = await _integrationEventLogContext.IntegrationEventLogs
            .Where(e => e.TransactionId == tid && e.State == EventState.NotPublished).ToListAsync();

        if (result.Any())
        {
            return result.OrderBy(o => o.CreationTime)
                .Select(e => e.DeserializeJsonContent(_eventTypes.Find(t => t.Name == e.EventTypeShortName)!));
        }

        return new List<IntegrationEventLogEntry>();
    }

    /// <summary>
    /// The SaveEventAsync.
    /// </summary>
    /// <param name="@event">The event<see cref="IntegrationEvent"/>.</param>
    /// <param name="transaction">The transaction<see cref="IDbContextTransaction"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

        _integrationEventLogContext.Database.UseTransaction(transaction.GetDbTransaction());
        _integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

        return _integrationEventLogContext.SaveChangesAsync();
    }

    /// <summary>
    /// The MarkEventAsPublishedAsync.
    /// </summary>
    /// <param name="eventId">The eventId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task MarkEventAsPublishedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.Published);
    }

    /// <summary>
    /// The MarkEventAsInProgressAsync.
    /// </summary>
    /// <param name="eventId">The eventId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task MarkEventAsInProgressAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.InProgress);
    }

    /// <summary>
    /// The MarkEventAsFailedAsync.
    /// </summary>
    /// <param name="eventId">The eventId<see cref="Guid"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task MarkEventAsFailedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.PublishedFailed);
    }

    /// <summary>
    /// The UpdateEventStatus.
    /// </summary>
    /// <param name="eventId">The eventId<see cref="Guid"/>.</param>
    /// <param name="status">The status<see cref="EventState"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private Task UpdateEventStatus(Guid eventId, EventState status)
    {
        var eventLogEntry = _integrationEventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == eventId);
        eventLogEntry.State = status;

        if (status == EventState.InProgress)
            eventLogEntry.TimesSent++;

        _integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

        return _integrationEventLogContext.SaveChangesAsync();
    }

    /// <summary>
    /// The Dispose.
    /// </summary>
    /// <param name="disposing">The disposing<see cref="bool"/>.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _integrationEventLogContext?.Dispose();
            }


            _disposedValue = true;
        }
    }

    /// <summary>
    /// The Dispose.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}