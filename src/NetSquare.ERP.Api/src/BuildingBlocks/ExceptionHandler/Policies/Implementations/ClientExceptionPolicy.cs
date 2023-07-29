//-----------------------------------------------------------------------
// <copyright file="ClientExceptionPolicy.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Policies.Implementations;

/// <summary>
/// Defines the <see cref="ClientExceptionPolicy" />.
/// </summary>
public class ClientExceptionPolicy : IExceptionHandler
{
    /// <summary>
    /// The ExecutePolicy.
    /// </summary>
    /// <param name="ex">The ex<see cref="Exception" />.</param>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <param name="isHandledException">The isHandledException<see cref="bool" />.</param>
    /// <param name="genericMessage">The genericMessage<see cref="string" />.</param>
    /// <returns>The <see cref="MessagesSummary" />.</returns>
    public MessagesSummary ExecutePolicy(
    Exception ex,
    HttpContext httpContext,
    bool isHandledException,
    string genericMessage)
    {
        MessagesSummary messageSummary = new()
        {
            Type = ExceptionTypes.Application.ToString(),
            StatusCode = httpContext.Response.StatusCode,
            TraceId = httpContext.TraceIdentifier,
            Messages = isHandledException
            ? UpdateMessagesCodeByExceptionType(
                httpContext,
                JsonConvert.DeserializeObject<MessagesSummary>(ex.Message),
                genericMessage)?.Messages
            : PopulateMessages(ex, httpContext, genericMessage)
        };

        return messageSummary;
    }

    /// <summary>
    /// The PopulateMessages.
    /// </summary>
    /// <param name="ex">The ex<see cref="Exception" />.</param>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <param name="genericMessage">The genericMessage<see cref="string" />.</param>
    /// <returns>The <see cref="List{Message}" />.</returns>
    private static List<Message> PopulateMessages(Exception ex, HttpContext httpContext, string genericMessage)
    {
        var messages = new List<Message>();
        if (ex.Data.Count > 0)
        {
            return UpdateMessagesCodeByExceptionType(httpContext, JsonConvert.DeserializeObject<Response>(ex.InnerException.Data["Response"].ToString()).MessagesSummary, genericMessage)?.Messages;
        }

        var message = ExceptionTypes.Application.GenerateMessageByExceptionType(httpContext);
        message.Text = $"{genericMessage}";
        message.Source = httpContext.Request.Path;
        message.MessageDisplayType = MessageDisplayTypes.Client.ToString();
        message.MessageIndicatorType = MessageIndicatorTypes.Error.ToString();
        messages.Add(message);
        return messages;
    }

    /// <summary>
    /// The UpdateMessagesCodeByExceptionType.
    /// </summary>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <param name="messagesSummary">The messagesSummary<see cref="MessagesSummary" />.</param>
    /// <param name="genericMessage">The genericMessage<see cref="string" />.</param>
    /// <returns>The <see cref="MessagesSummary" />.</returns>
    private static MessagesSummary UpdateMessagesCodeByExceptionType(
    HttpContext httpContext,
    MessagesSummary messagesSummary,
    string genericMessage)
    {
        foreach (var msg in messagesSummary?.Messages)
        {
            if (msg is null)
            {
                continue;
            }

            Enum.TryParse(
                Regex.Split(msg?.Title?.Trim() ?? string.Empty, @"\s")
                    .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)),
                true,
                out ExceptionTypes exceptionType);
            msg.Code = string.IsNullOrWhiteSpace(msg.Code)
                ? exceptionType.GenerateMessageByExceptionType(httpContext).Code
                : msg.Code;
            msg.MessageDisplayType = string.IsNullOrWhiteSpace(msg.MessageDisplayType)
                ? MessageDisplayTypes.Client.ToString()
                : msg.MessageDisplayType;
            msg.MessageIndicatorType = string.IsNullOrWhiteSpace(msg.MessageIndicatorType)
                ? MessageIndicatorTypes.Error.ToString()
                : msg.MessageIndicatorType;
            msg.Source = string.IsNullOrWhiteSpace(msg.Source) ? httpContext.Request.Path : msg.Source;
            msg.Text = exceptionType == ExceptionTypes.Connection ? genericMessage : msg.Text;
        }

        return messagesSummary;
    }
}