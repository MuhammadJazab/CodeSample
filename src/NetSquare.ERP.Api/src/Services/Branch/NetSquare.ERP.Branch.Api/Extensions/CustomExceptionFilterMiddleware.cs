//-----------------------------------------------------------------------
// <copyright file="ConfigureAutoFacContainerExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Extensions;

/// <summary>
/// Defines the <see cref="CustomExceptionFilterMiddleware" />.
/// </summary>    
public class CustomExceptionFilterMiddleware
{
    /// <summary>
    /// Defines the next.
    /// </summary>
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomExceptionFilterMiddleware"/> class.
    /// </summary>
    /// <param name="next">.</param>
    public CustomExceptionFilterMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// InvokeAsync.
    /// </summary>
    /// <param name="context">.</param>
    /// <returns>.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// The HandleExceptionAsync.
    /// </summary>
    /// <param name="context">The context<see cref="HttpContext"/>.</param>
    /// <param name="exception">The exception<see cref="Exception"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var customErrorMessage = exception switch
        {
            _ => "An unhandled exception has occurred."
        };

        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            CustomErrorMessage = customErrorMessage,
            ExceptionMessage = exception.Message,
            StackTrace = exception.StackTrace!
        }.ToString());
    }
}

/// <summary>
/// Defines the <see cref="ErrorDetails" />.
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Gets or sets the StatusCode.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the CustomErrorMessage.
    /// </summary>
    public string CustomErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ExceptionMessage.
    /// </summary>
    public string ExceptionMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the StackTrace.
    /// </summary>
    public string StackTrace { get; set; } = string.Empty;

    /// <summary>
    /// The ToString.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}