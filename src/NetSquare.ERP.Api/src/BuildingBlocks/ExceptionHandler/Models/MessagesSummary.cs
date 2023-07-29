//-----------------------------------------------------------------------
// <copyright file="MessagesSummary.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Models;

/// <summary>
/// Defines the <see cref="MessagesSummary" />.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class MessagesSummary
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagesSummary"/> class.
    /// </summary>
    public MessagesSummary()
    {
        this.Messages = new List<Message>();
    }

    /// <summary>
    /// Gets or sets the StatusCode
    /// Gets or sets the StatusCode..
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the Type
    /// Gets or sets the Type..
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the TraceId
    /// Gets or sets the TraceId..
    /// </summary>
    public string TraceId { get; set; }

    /// <summary>
    /// Gets or sets the Messages
    /// Gets or sets the Messages..
    /// </summary>
    public List<Message> Messages { get; set; }

    /// <summary>
    /// The ToString.
    /// </summary>
    /// <returns>The <see cref="string" />.</returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}