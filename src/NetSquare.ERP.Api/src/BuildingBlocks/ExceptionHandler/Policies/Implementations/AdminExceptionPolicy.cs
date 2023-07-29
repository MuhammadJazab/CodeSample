//-----------------------------------------------------------------------
// <copyright file="AdminExceptionPolicy.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Policies.Implementations;

/// <summary>
/// Defines the <see cref="AdminExceptionPolicy" />.
/// </summary>
public partial class AdminExceptionPolicy : IExceptionHandler
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
        var messageSummary = new MessagesSummary
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
            return UpdateMessagesCodeByExceptionType(
                httpContext,
                JsonConvert.DeserializeObject<Response>(ex.Data["Response"].ToString()).MessagesSummary,
                genericMessage)?.Messages;
        }

        var message = ExceptionTypes.Application.GenerateMessageByExceptionType(httpContext);
        message.Text = $"{genericMessage} {ex}";
        message.Source = httpContext.Request.Path;
        message.MessageDisplayType = MessageDisplayTypes.Admin.ToString();
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
                MyRegex().Split(msg?.Title?.Trim() ?? string.Empty)
                    .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)),
                true,
                out ExceptionTypes exceptionType);
            msg.Code = string.IsNullOrWhiteSpace(msg.Code)
                ? exceptionType.GenerateMessageByExceptionType(httpContext).Code
                : msg.Code;
            msg.MessageDisplayType = string.IsNullOrWhiteSpace(msg.MessageDisplayType)
                ? MessageDisplayTypes.Admin.ToString()
                : msg.MessageDisplayType;
            msg.MessageIndicatorType = string.IsNullOrWhiteSpace(msg.MessageIndicatorType)
                ? MessageIndicatorTypes.Error.ToString()
                : msg.MessageIndicatorType;
            msg.Source = string.IsNullOrWhiteSpace(msg.Source) ? httpContext.Request.Path : msg.Source;
            msg.Text = exceptionType == ExceptionTypes.Connection ? $"{genericMessage}{msg.Text}" : msg.Text;
        }

        return messagesSummary;
    }

    [GeneratedRegex("\\s")]
    private static partial Regex MyRegex();
}