//-----------------------------------------------------------------------
// <copyright file="TestIntegrationEvent.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace EventBus.Test.Core.Fixtures;

/// <summary>
/// Defines the <see cref="TestIntegrationEvent" />.
/// </summary>
public record TestIntegrationEvent : IntegrationEvent
{
    /// <summary>
    /// Defines the Exchange
    /// </summary>
    public string Exchange { get; set; }

    /// <summary>
    /// Defines the RoutingKey
    /// </summary>
    public string RoutingKey { get; set; }

    /// <summary>
    /// Defines the Mandatory
    /// </summary>
    public bool Mandatory { get; set; }

    /// <summary>
    /// Defines the BasicProperties
    /// </summary>
    public IBasicProperties BasicProperties { get; set; }

    /// <summary>
    /// Defines the Body
    /// </summary>
    public ReadOnlyMemory<byte> Body { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestIntegrationEvent"/> class.
    /// </summary>
    /// <param name="exchage"><see cref="string"/></param>
    /// <param name="routingKey"><see cref="string"/></param>
    /// <param name="mandatory"><see cref="bool"/></param>
    /// <param name="basicProperties"><see cref="IBasicProperties"/></param>
    /// <param name="body"><see cref="ReadOnlyMemory<byte>"/></param>
    public TestIntegrationEvent(string exchage, string routingKey, bool mandatory, IBasicProperties basicProperties, ReadOnlyMemory<byte> body)
    {
        this.Exchange = exchage;
        this.RoutingKey = routingKey;
        this.Mandatory = mandatory;
        this.BasicProperties = basicProperties;
        this.Body = body;
    }
}