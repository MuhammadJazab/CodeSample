//-----------------------------------------------------------------------
// <copyright file="IntegrationEventHandlerFixture.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBus.Test.Core.Fixtures;

/// <summary>
/// Defines the <see cref="IntegrationEventFixture" />.
/// </summary>
public record IntegrationEventFixture : IntegrationEvent
{
    public string Message { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationEventFixture" /> class.
    /// </summary>
    /// <param name="message"><see cref="string"/></param>
    public IntegrationEventFixture(string message)
    {
        this.Message = message;
    }
}