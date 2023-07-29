//-----------------------------------------------------------------------
// <copyright file="MessageExtensions.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Models;

/// <summary>
/// Message Summary Extensions.
/// </summary>
public static class MessageExtensions
{
    /// <summary>
    /// Determines whether the specified messages has information.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public static bool HasInformation(this List<Message> messages)
    {
        if (messages == null)
        {
            return false;
        }
        else
        {
            return messages.Any(m => string.Equals(m.MessageIndicatorType, MessageIndicatorTypes.Information.ToString(), System.StringComparison.InvariantCultureIgnoreCase));
        }
    }

    /// <summary>
    /// Determines whether the specified messages has warning.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public static bool HasWarning(this List<Message> messages)
    {
        if (messages == null)
        {
            return false;
        }
        else
        {
            return messages.Any(m => string.Equals(m.MessageIndicatorType, MessageIndicatorTypes.Warning.ToString(), System.StringComparison.InvariantCultureIgnoreCase));
        }
    }

    /// <summary>
    /// Errorses the count.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns>Error count.</returns>
    public static int ErrorsCount(this List<Message> messages)
    {
        if (messages == null)
        {
            return 0;
        }
        else
        {
            return messages.Count(m => string.Equals(m.MessageIndicatorType, MessageIndicatorTypes.Error.ToString(), System.StringComparison.InvariantCultureIgnoreCase));
        }
    }

    /// <summary>
    /// Determines whether the specified messages has errors.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public static bool HasErrors(this List<Message> messages)
    {
        if (messages == null)
        {
            return false;
        }
        else
        {
            return messages.Any(m => string.Equals(m.MessageIndicatorType, MessageIndicatorTypes.Error.ToString(), System.StringComparison.InvariantCultureIgnoreCase));
        }
    }

    /// <summary>
    /// Gets the error messages.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns>The list of messages.</returns>
    public static List<Message> GetErrorMessages(this List<Message> messages)
    {
        if (messages == null || messages.Count == 0)
        {
            return new List<Message>();
        }
        else
        {
            return messages.Where(m => string.Equals(m.MessageIndicatorType, MessageIndicatorTypes.Error.ToString(), System.StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }

    /// <summary>
    /// Gets the warning messages.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns>The list of messages.</returns>
    public static List<Message> GetWarningMessages(this List<Message> messages)
    {
        if (messages == null || messages.Count == 0)
        {
            return new List<Message>();
        }
        else
        {
            return messages.Where(m => string.Equals(m.MessageIndicatorType, MessageIndicatorTypes.Warning.ToString(), System.StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }

    /// <summary>
    /// Gets the information messages.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <returns>The list of messages.</returns>
    public static List<Message> GetInformationMessages(this List<Message> messages)
    {
        if (messages == null || messages.Count == 0)
        {
            return new List<Message>();
        }
        else
        {
            return messages.Where(m => string.Equals(m.MessageIndicatorType, MessageIndicatorTypes.Information.ToString(), System.StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }

    /// <summary>
    /// Gets the type of the messages of.
    /// </summary>
    /// <param name="messages">The messages.</param>
    /// <param name="messageTypeIndicator">The message type indicator.</param>
    /// <returns>The list of messages.</returns>
    public static List<Message> GetMessagesOfType(this List<Message> messages, MessageIndicatorTypes messageTypeIndicator)
    {
        if (messages == null || messages.Count == 0)
        {
            return new List<Message>();
        }
        else
        {
            return messages.Where(m => string.Equals(m.MessageIndicatorType, messageTypeIndicator.ToString(), System.StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }
}