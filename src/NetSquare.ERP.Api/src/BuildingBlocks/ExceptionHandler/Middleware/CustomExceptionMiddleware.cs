//-----------------------------------------------------------------------
// <copyright file="CustomExceptionMiddleware.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Middleware;

/// <summary>
/// Defines the <see cref="CustomExceptionMiddleware" />.
/// </summary>
public class CustomExceptionMiddleware
{
    /// <summary>
    /// Defines the exceptionPolicies..
    /// </summary>
    private readonly IIndex<string, IExceptionHandler> exceptionPolicies;

    /// <summary>
    /// Defines the logger..
    /// </summary>
    private readonly ILogger logger;

    /// <summary>
    /// Defines the next..
    /// </summary>
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next<see cref="RequestDelegate" />.</param>
    /// <param name="loggerFactory">The loggerFactory<see cref="ILoggerFactory" />.</param>
    /// <param name="exceptionPolicies">The exceptionPolicies<see cref="IIndex{String, IExceptionHandler}" />.</param>
    public CustomExceptionMiddleware(
    RequestDelegate next,
    ILoggerFactory loggerFactory,
    IIndex<string, IExceptionHandler> exceptionPolicies)
    {
        this.next = next;
        this.logger = loggerFactory.CreateLogger<CustomExceptionMiddleware>();
        this.exceptionPolicies = exceptionPolicies;
    }

    /// <summary>
    /// The InvokeAsync.
    /// </summary>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <returns>The <see cref="Task" />.</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await this.next(httpContext);
            await HandleUnAuthorizationAsync(httpContext);
        }
        catch (Exception exception)
        {
            await this.HandleExceptionAsync(httpContext, exception);
        }
    }

    /// <summary>
    /// The HandleUnAuthorizationAsync.
    /// </summary>
    /// <param name="context">The context<see cref="HttpContext" />.</param>
    /// <returns>The <see cref="Task" />.</returns>
    private static async Task HandleUnAuthorizationAsync(HttpContext context)
    {
        if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            context.Response.Body.Position = 0;
            var authorizationMessage = new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Position = 0;

            var response = new Response();
            var message = ExceptionTypes.Authentication.GenerateMessageByExceptionType(context);
            message.Source = context.Request.Path;
            message.Text = string.IsNullOrEmpty(authorizationMessage.Result) ? "Access is denied due to invalid or missing token." : authorizationMessage.Result;
            message.MessageDisplayType = MessageDisplayTypes.All.ToString();
            message.MessageIndicatorType = MessageIndicatorTypes.Error.ToString();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            var messageSummary = new MessagesSummary
            {
                Type = ExceptionTypes.Authentication.ToString(),
                StatusCode = context.Response.StatusCode,
                TraceId = context.TraceIdentifier,
                Messages = new List<Message>
                {
                    message,
                },
            };
            response.MessagesSummary = messageSummary;

            var jsonResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
            context.Response.ContentLength = jsonResponse.Length;
            await context.Response.WriteAsync(jsonResponse);

        }
    }

    /// <summary>
    /// The HandleExceptionAsync.
    /// </summary>
    /// <param name="context">The context<see cref="HttpContext" />.</param>
    /// <param name="ex">The ex<see cref="Exception" />.</param>
    /// <returns>The <see cref="Task" />.</returns>
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = new Response();
        var exception = ex.InnerException ?? ex;

        var genericMessage = "An exception has occurred while processing the request.";

        context.Response.ContentType = "application/json";
        switch (exception)
        {
            case AppException:
                // custom application error
                this.logger.LogInformation("{genericMessage} {exception}, correlationId: {correlationId}", genericMessage, exception, context.TraceIdentifier);
                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError; //// Handled inernal application message summary errors
                response.MessagesSummary = context.GeneratePolicyExceptions(
                    this.exceptionPolicies,
                    exception,
                    true,
                    genericMessage);
                break;
            case NotFoundException:
                genericMessage = "No recored(s) found.";
                this.logger.LogInformation("{genericMessage} {exception}, correlationId: {correlationId}", genericMessage, exception, context.TraceIdentifier);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response.MessagesSummary = context.GeneratePolicyExceptions(
                    this.exceptionPolicies,
                    exception,
                    true,
                    genericMessage);
                break;
            case ConnectionException:
                genericMessage = "The underlying connection was closed: An unexpected error occurred on a receive.";
                this.logger.LogInformation("{genericMessage} {exception}, correlationId: {correlationId}", genericMessage, exception, context.TraceIdentifier);
                context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                response.MessagesSummary = context.GeneratePolicyExceptions(
                    this.exceptionPolicies,
                    exception,
                    true,
                    genericMessage);
                break;
            default:
                // unhandled error
                genericMessage = "An unhandled exception has occurred while processing the request.";
                this.logger.LogInformation("{genericMessage} {exception}, correlationId: {correlationId}", genericMessage, exception, context.TraceIdentifier);
                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError; //// Unhandled internal application exceptions
                response.MessagesSummary = context.GeneratePolicyExceptions(
                    this.exceptionPolicies,
                    exception,
                    false,
                    genericMessage);
                break;
        }

        return context.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
    }
}