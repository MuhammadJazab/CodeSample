//-----------------------------------------------------------------------
// <copyright file="MessageSummaryExtensions.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Models;

/// <summary>
/// Message Summary Extensions.
/// </summary>
public static class MessageSummaryExtensions
{
    /// <summary>
    /// Determines whether the specified messages summary has information.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns>
    ///   <c>true</c> if the specified messages summary has information; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasInformation(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return false;
        }
        else
        {
            return messagesSummary.Messages.HasInformation();
        }
    }

    /// <summary>
    /// Determines whether the specified messages summary has warning.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns>
    ///   <c>true</c> if the specified messages summary has warning; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasWarning(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return false;
        }
        else
        {
            return messagesSummary.Messages.HasWarning();
        }
    }

    /// <summary>
    /// Errorses the count.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns>Error count.</returns>
    public static int ErrorsCount(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return 0;
        }
        else
        {
            return messagesSummary.Messages.ErrorsCount();
        }
    }

    /// <summary>
    /// Determines whether the specified messages summary has errors.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns>
    ///   <c>true</c> if the specified messages summary has errors; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasErrors(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return false;
        }
        else
        {
            return messagesSummary.Messages.HasErrors();
        }
    }

    /// <summary>
    /// Gets the error messages.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns>list of error messages.</returns>
    public static List<Message> GetErrorMessages(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return new List<Message>();
        }
        else
        {
            return messagesSummary.Messages.GetErrorMessages();
        }
    }

    /// <summary>
    /// Gets the warning messages.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns>list of messages.</returns>
    public static List<Message> GetWarningMessages(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return new List<Message>();
        }
        else
        {
            return messagesSummary.Messages.GetWarningMessages();
        }
    }

    /// <summary>
    /// Gets the information messages.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns>list of messages.</returns>
    public static List<Message> GetInformationMessages(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return new List<Message>();
        }
        else
        {
            return messagesSummary.Messages.GetInformationMessages();
        }
    }

    /// <summary>
    /// Gets the type of the messages of.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <param name="messageTypeIndicator">The message type indicator.</param>
    /// <returns>list of messages.</returns>
    public static List<Message> GetMessagesOfType(this MessagesSummary messagesSummary, MessageIndicatorTypes messageTypeIndicator)
    {
        if (messagesSummary == null)
        {
            return new List<Message>();
        }
        else
        {
            return messagesSummary.Messages.GetMessagesOfType(messageTypeIndicator);
        }
    }

    /// <summary>
    /// Determines whether messages summary contains any mininum/maximum group size or policy error.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns><c>true</c> if contains such errors, otherwise <c>false</c>.</returns>
    public static bool ContainsMinMaxOrPolicyChangeError(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return false;
        }

        return messagesSummary.Messages.Any(f =>
            StringComparer.OrdinalIgnoreCase.Equals(f.Code, "1138") ||
            StringComparer.OrdinalIgnoreCase.Equals(f.Code, "1139") ||
            StringComparer.OrdinalIgnoreCase.Equals(f.Code, "1140") ||
            StringComparer.OrdinalIgnoreCase.Equals(f.Code, "1141"));
    }

    /// <summary>
    /// Determines whether any messages contain error code for max number of ticket reissues.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <returns><c>true</c> if contains such error code, otherwise <c>false</c>.</returns>
    public static bool ContainsMaxNumberIfTicketReissuesError(this MessagesSummary messagesSummary)
    {
        if (messagesSummary == null)
        {
            return false;
        }

        return messagesSummary.Messages.Any(f =>
            StringComparer.OrdinalIgnoreCase.Equals(f.Code, "8028"));
    }

    /// <summary>
    /// Clears all messages.
    /// </summary>
    /// <param name="messagesSummary">The messages summary.</param>
    /// <param name="codeExceptions">The code exceptions.</param>
    public static void ClearAllMessages(this MessagesSummary messagesSummary, params string[] codeExceptions)
    {
        if (messagesSummary == null)
        {
            return;
        }

        codeExceptions ??= Array.Empty<string>();

        IList<Message> filteredMessages = new List<Message>();

        foreach (Message message in messagesSummary.Messages)
        {
            if (codeExceptions != null && codeExceptions.Contains(message.Code))
            {
                filteredMessages.Add(message);
            }
        }

        messagesSummary.Messages = filteredMessages.ToList();
    }

    /// <summary>
    /// Add message to message summary.
    /// </summary>
    /// <param name="messagesSummary">The message summary.</param>
    /// <param name="message">The message.</param>
    public static void AddMessage(this MessagesSummary messagesSummary, Message message)
    {
        if (message != null)
        {
            messagesSummary.Messages ??= new List<Message>();

            messagesSummary.Messages.Add(message);
        }
    }
}