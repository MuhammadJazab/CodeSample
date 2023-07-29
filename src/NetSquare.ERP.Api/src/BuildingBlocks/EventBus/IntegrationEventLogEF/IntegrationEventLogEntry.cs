//-----------------------------------------------------------------------
// <copyright file="IntegrationEventLogEntry.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.IntegrationEventLogEF;

/// <summary>
/// Defines the <see cref="IntegrationEventLogEntry" />.
/// </summary>
public class IntegrationEventLogEntry
{
    /// <summary>
    /// Prevents a default instance of the <see cref="IntegrationEventLogEntry"/> class from being created.
    /// </summary>
    private IntegrationEventLogEntry()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationEventLogEntry"/> class.
    /// </summary>
    /// <param name="@event">The event<see cref="IntegrationEvent"/>.</param>
    /// <param name="transactionId">The transactionId<see cref="Guid"/>.</param>
    public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
    {
        EventId = @event.Id;
        CreationTime = @event.CreationDate;
        EventTypeName = @event.GetType().FullName!;
        Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });
        State = EventState.NotPublished;
        TimesSent = 0;
        TransactionId = transactionId.ToString();
    }

    /// <summary>
    /// Gets the EventId.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets the EventTypeName.
    /// </summary>
    public string EventTypeName { get; set; }

    /// <summary>
    /// Gets the EventTypeShortName.
    /// </summary>
    [NotMapped]
    public string? EventTypeShortName => EventTypeName.Split('.')?.Last();

    /// <summary>
    /// Gets the IntegrationEvent.
    /// </summary>
    [NotMapped]
    public IntegrationEvent IntegrationEvent { get; private set; }

    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    public EventState State { get; set; }

    /// <summary>
    /// Gets or sets the TimesSent.
    /// </summary>
    public int TimesSent { get; set; }

    /// <summary>
    /// Gets the CreationTime.
    /// </summary>
    public DateTime CreationTime { get; private set; }

    /// <summary>
    /// Gets the Content.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Gets the TransactionId.
    /// </summary>
    public string TransactionId { get; private set; }

    /// <summary>
    /// The DeserializeJsonContent.
    /// </summary>
    /// <param name="type">The type<see cref="Type"/>.</param>
    /// <returns>The <see cref="IntegrationEventLogEntry"/>.</returns>
    public IntegrationEventLogEntry DeserializeJsonContent(Type type)
    {
        IntegrationEvent = JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) as IntegrationEvent;
        return this;
    }
}