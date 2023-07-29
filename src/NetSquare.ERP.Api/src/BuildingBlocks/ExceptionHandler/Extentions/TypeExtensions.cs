//-----------------------------------------------------------------------
// <copyright file="RegisterExceptionPoliciesModules.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Extentions;

/// <summary>
/// Defines the <see cref="TypeExtensions" />.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// The GetDescription.
    /// </summary>
    /// <typeparam name="T">The T.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns>The <see cref="string" />The T.</returns>    
    public static string GetDescription<T>(this T source)
    {
        var fi = source.GetType().GetField(source.ToString());

        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
        {
            return attributes[0].Description;
        }

        return source.ToString();
    }

    /// <summary>
    /// The GenerateMessageByExceptionType.
    /// </summary>
    /// <param name="source">The source<see cref="Enum" />.</param>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <returns>The <see cref="Message" />.</returns>
    public static Message GenerateMessageByExceptionType(this Enum source, HttpContext httpContext)
    {
        var fi = source.GetType().GetField(source.ToString());
        var message = new Message();

        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
        {
            message.Title = attributes[0].Description;
        }
        else
        {
            message.Title = source.ToString();
        }

        return GenerateMessageCodeByExceptionType(source, message, httpContext);
    }

    /// <summary>
    /// The GenerateMessageCodeByExceptionType.
    /// </summary>
    /// <param name="source">The source<see cref="Enum" />.</param>
    /// <param name="message">The message<see cref="Message" />.</param>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <returns>The <see cref="Message" />.</returns>
    private static Message GenerateMessageCodeByExceptionType(Enum source, Message message, HttpContext httpContext)
    {
        var matchRegex = new Regex($"{Applications.Gateway}|{Applications.Authentication}|{Applications.Branch}|{Applications.Department}|{Applications.HumanResource}|{Applications.gRPCService}", RegexOptions.IgnoreCase, matchTimeout: TimeSpan.FromMilliseconds(10));
        var matched = matchRegex.Match(httpContext.Request.Path);

        if (matched.Success)
        {
            Enum.TryParse(
                httpContext.Request.Path.Value.ToLower().Contains(Applications.Web.ToString().ToLower())
                    ? Applications.Web.ToString()
                    : matched.Value,
                true,
                out Applications application);
            var exceptionCode = application.GetDescription();
            switch (source)
            {
                case ExceptionTypes.Connection:
                    message.Code = $"{exceptionCode}0001-{exceptionCode}-1000";
                    return message;
                case ExceptionTypes.Authentication:
                    message.Code = $"{exceptionCode}0000-{exceptionCode}-0000";
                    return message;
                case ExceptionTypes.Validation:
                    message.Code = $"{exceptionCode}0002-{exceptionCode}-2000";
                    return message;
                case ExceptionTypes.Application:
                    message.Code = $"{exceptionCode}0003-{exceptionCode}-3000";
                    return message;
            }
        }

        return message;
    }
}