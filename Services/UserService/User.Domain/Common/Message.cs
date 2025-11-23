
//file="Message.cs" >

namespace User.Domain.Common;
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
        this.MessageIndicatorType = nameof(MessageIndicatorTypes.Error);
        this.MessageDisplayType = nameof(MessageDisplayTypes.All);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <param name="messageIndicatorType">The messageIndicatorType<see cref="MessageIndicatorTypes"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    public Message(string message, MessageIndicatorTypes messageIndicatorType, MessageDisplayTypes messageDisplayType)
    : this(message, string.Empty, messageIndicatorType, messageDisplayType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="code">The code<see cref="string"/>.</param>
    /// <param name="title">The title<see cref="string"/>.</param>
    /// <param name="text">The text<see cref="string"/>.</param>
    /// <param name="messageIndicatorTypes">The messageIndicatorTypes<see cref="MessageIndicatorTypes"/>.</param>
    public Message(string code, string title, string text, MessageIndicatorTypes messageIndicatorTypes)
    {
        this.Code = code;
        this.Title = title;
        this.Text = text;
        this.MessageIndicatorType = nameof(messageIndicatorTypes);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <param name="messageTitle">The messageTitle<see cref="string"/>.</param>
    public Message(string message, string messageTitle)
   : this(message, messageTitle, MessageIndicatorTypes.Error, MessageDisplayTypes.All)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <param name="messageTitle">The messageTitle<see cref="string"/>.</param>
    /// <param name="clientFacingMessage">The clientFacingMessage<see cref="string"/>.</param>
    /// <param name="messageIndicatorType">The messageIndicatorType<see cref="MessageIndicatorTypes"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    public Message(string message, string messageTitle, MessageIndicatorTypes messageIndicatorType, MessageDisplayTypes messageDisplayType)
    {
        this.Text = message;
        this.Title = messageTitle;
        this.MessageIndicatorType = nameof(messageIndicatorType);
        this.MessageDisplayType = nameof(messageDisplayType);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <param name="messageTitle">The messageTitle<see cref="string"/>.</param>
    /// <param name="messageIndicatorType">The messageIndicatorType<see cref="MessageIndicatorTypes"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    /// <param name="code">The code<see cref="string"/>.</param>
    public Message(string message, string messageTitle, MessageIndicatorTypes messageIndicatorType, MessageDisplayTypes messageDisplayType, string code)
    {
        this.Text = message;
        this.Title = messageTitle;
        this.MessageIndicatorType = nameof(messageIndicatorType);
        this.MessageDisplayType = nameof(messageDisplayType);
        this.Code = code;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    /// <param name="messageIndicatorType">The messageIndicatorType<see cref="MessageIndicatorTypes"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    public Message(MessageIndicatorTypes messageIndicatorType, MessageDisplayTypes messageDisplayType)
    : this(string.Empty, string.Empty, messageIndicatorType, messageDisplayType)
    {
    }

    /// <summary>
    /// Gets or sets the Title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Text.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Code.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Source.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the MessageIndicatorType.
    /// </summary>
    public string MessageIndicatorType { get; set; }

    /// <summary>
    /// Gets or sets the MessageDisplayType.
    /// </summary>
    public string MessageDisplayType { get; set; }
}

