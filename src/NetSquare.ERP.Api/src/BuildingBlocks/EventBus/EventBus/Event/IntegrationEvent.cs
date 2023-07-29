//-----------------------------------------------------------------------
// <copyright file="IntegrationEvent.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus.Events;

/// <summary>
/// IntegrationEvent
/// </summary>
public record IntegrationEvent
{
    /// <summary>
    /// IntegrationEvent Constructor
    /// </summary>
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    /// <summary>
    /// IntegrationEvent Parameterized Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="createDate"></param>
    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime createDate)
    {
        Id = id;
        CreationDate = createDate;
    }

    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [JsonInclude]
    public Guid Id { get; private init; }

    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [JsonInclude]
    public DateTime CreationDate { get; private init; }
}