//-----------------------------------------------------------------------
// <copyright file="MessagesSummary.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Common;

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
        Messages = new List<Message>();
    }

    /// <summary>
    /// Gets or sets the StatusCode.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the TraceId.
    /// </summary>
    public string TraceId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Messages.
    /// </summary>
    public List<Message> Messages { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether IsValid.
    /// </summary>
    [DataMember(Name = "IsValid")]
    public bool IsValid
    {
        get
        {
            Valid = Messages.Find(msg => msg.MessageIndicatorType == MessageIndicatorTypes.Error) == null || !Messages.Exists(msg => msg.MessageIndicatorType == MessageIndicatorTypes.Error);
            return Valid;
        }
        set
        {
            Valid = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Valid.
    /// </summary>
    [DataMember(Name = "Valid")]
    private bool Valid { get; set; }

    /// <summary>
    /// The FromError.
    /// </summary>
    /// <param name="errorText">The errorText<see cref="string"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    /// <returns>The <see cref="MessagesSummary"/>.</returns>
    public static MessagesSummary FromError(string errorText, MessageDisplayTypes messageDisplayType = MessageDisplayTypes.All)
    {
        var messagesSummary = new MessagesSummary();
        messagesSummary.Messages.Add(new Message { Text = errorText, MessageIndicatorType = MessageIndicatorTypes.Error, MessageDisplayType = messageDisplayType });
        return messagesSummary;
    }

    /// <summary>
    /// The AddErrorForAll.
    /// </summary>
    /// <param name="errorMessage">The errorMessage<see cref="string"/>.</param>
    public void AddErrorForAll(string errorMessage)
    {
        Messages ??= new List<Message>();

        Messages.Add(new Message()
        {
            Text = errorMessage,
            MessageIndicatorType = MessageIndicatorTypes.Error,
            MessageDisplayType = MessageDisplayTypes.All,
        });
    }

    /// <summary>
    /// The AddInformationForAll.
    /// </summary>
    /// <param name="message">The message<see cref="string"/>.</param>
    /// <param name="code">The code<see cref="string"/>.</param>
    public void AddInformationForAll(string message, string code = "")
    {
        Messages ??= new List<Message>();

        Messages.Add(new Message()
        {
            Text = message,
            MessageIndicatorType = MessageIndicatorTypes.Information,
            MessageDisplayType = MessageDisplayTypes.All,
            Code = code,
        });
    }

    /// <summary>
    /// The AddError.
    /// </summary>
    /// <param name="errorMessage">The errorMessage<see cref="string"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    public void AddError(string errorMessage, MessageDisplayTypes messageDisplayType)
    {
        Messages ??= new List<Message>();

        Messages.Add(new Message()
        {
            Text = errorMessage,
            MessageIndicatorType = MessageIndicatorTypes.Error,
            MessageDisplayType = messageDisplayType,
        });
    }

    /// <summary>
    /// The AddError.
    /// </summary>
    /// <param name="errorMessage">The errorMessage<see cref="string"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    /// <param name="code">The code<see cref="string"/>.</param>
    public void AddError(string errorMessage, MessageDisplayTypes messageDisplayType, string code)
    {
        Messages ??= new List<Message>();

        Messages.Add(new Message()
        {
            Text = errorMessage,
            MessageIndicatorType = MessageIndicatorTypes.Error,
            MessageDisplayType = messageDisplayType,
            Code = code,
        });
    }

    /// <summary>
    /// The Add.
    /// </summary>
    /// <param name="message">The message<see cref="Message"/>.</param>
    public void Add(Message message)
    {
        Messages ??= new List<Message>();

        Messages.Add(message);
    }

    /// <summary>
    /// The AddRange.
    /// </summary>
    /// <param name="messages">The messages<see cref="IEnumerable{Message}"/>.</param>
    public void AddRange(IEnumerable<Message> messages)
    {
        Messages ??= new List<Message>();

        Messages.AddRange(messages);
    }

    /// <summary>
    /// The AddWarning.
    /// </summary>
    /// <param name="warningMessage">The warningMessage<see cref="string"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    public void AddWarning(string warningMessage, MessageDisplayTypes messageDisplayType, string code)
    {
        Messages ??= new List<Message>();

        ////No need to mark collection dirty since warnings never generate validation changes
        Messages.Add(new Message
        {
            Text = warningMessage,
            MessageIndicatorType = MessageIndicatorTypes.Warning,
            MessageDisplayType = messageDisplayType,
            Code = code
        });
    }

    /// <summary>
    /// The AddStackTrace.
    /// </summary>
    /// <param name="strackTrace">The strackTrace<see cref="string"/>.</param>
    /// <param name="messageDisplayType">The messageDisplayType<see cref="MessageDisplayTypes"/>.</param>
    public void AddStackTrace(string strackTrace, MessageDisplayTypes messageDisplayType = MessageDisplayTypes.ManagementCompany)
    {
        Messages ??= new List<Message>();

        Messages.Add(new Message()
        {
            Title = "Stack Trace",
            Code = "STKTC",
            Text = strackTrace,
            MessageIndicatorType = MessageIndicatorTypes.StackTrace,
            MessageDisplayType = messageDisplayType,
        });
    }

    /// <summary>
    /// The ToString.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString()
    {
        if (Messages.Count > 0)
        {
            return string.Join(" , ", Messages.Select(p => p.Text).ToArray());
        }
        else
        {
            return string.Empty;
        }
    }
}