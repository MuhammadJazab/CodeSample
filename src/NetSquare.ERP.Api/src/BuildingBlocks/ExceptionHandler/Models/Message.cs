//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Models;

/// <summary>
/// Defines the <see cref="Message" />.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class Message
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    public Message()
    {
        this.MessageIndicatorType = MessageIndicatorTypes.Error.ToString();
        this.MessageDisplayType = MessageDisplayTypes.All.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="code">The Code.</param>
    /// <param name="title">The title.</param>
    /// <param name="text">The text.</param>
    /// <param name="messageIndicatorTypes">The Message Indicator Type.</param>
    public Message(string code, string title, string text, string messageIndicatorTypes)
    {
        this.Code = code;
        this.Title = title;
        this.Text = text;
        this.MessageIndicatorType = messageIndicatorTypes;
    }

    /// <summary>
    /// Gets or sets the Title
    /// Gets or sets the Title..
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the Text
    /// Gets or sets the Text..
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the Code
    /// Gets or sets the Code..
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the Source
    /// Gets or sets the Source..
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the MessageIndicatorType
    /// Gets or sets the MessageIndicatorType..
    /// </summary>
    public string MessageIndicatorType { get; set; }

    /// <summary>
    /// Gets or sets the MessageDisplayType
    /// Gets or sets the MessageDisplayType..
    /// </summary>
    public string MessageDisplayType { get; set; }
}