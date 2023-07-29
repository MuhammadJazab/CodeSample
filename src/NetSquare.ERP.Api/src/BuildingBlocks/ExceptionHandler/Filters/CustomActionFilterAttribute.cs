//-----------------------------------------------------------------------
// <copyright file="CustomActionFilterAttribute.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Filters;

/// <summary>
/// Defines the <see cref="CustomActionFilterAttribute" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class CustomActionFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Defines the logger..
    /// </summary>
    private readonly ILogger<CustomActionFilterAttribute> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomActionFilterAttribute"/> class.
    /// </summary>
    /// <param name="logger">The logger<see cref="ILogger{CustomActionFilterAttribute}" />.</param>
    public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// The OnActionExecuted.
    /// </summary>
    /// <param name="context">The context<see cref="ActionExecutedContext" />.</param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // PENDING remove first if statement when going to live
        if (!context.HttpContext.Request.Path.Equals("/test/notification") && !(context.Result is ViewResult ||
            context.Result is ContentResult || context.Result is JsonResult))
        {
            var responseContext = (ObjectResult)context.Result;

            var response = JsonConvert.DeserializeObject<Response>(JsonConvert.SerializeObject(responseContext?.Value, Formatting.Indented));
            var messages = response?.MessagesSummary?.Messages;

            if (messages != null && messages.Any())
            {
                if (messages.Any(x => x.MessageIndicatorType == MessageIndicatorTypes.Error.ToString()))
                {
                    this.logger.LogInformation("Error Message");
                    throw new AppException(response!.MessagesSummary);
                }

                if (messages.Any(x => x.MessageIndicatorType == MessageIndicatorTypes.Information.ToString()))
                {
                    throw new NotFoundException(response!.MessagesSummary);
                }
            }
        }

        base.OnActionExecuted(context);
    }

    /// <summary>
    /// The OnActionExecuting.
    /// </summary>
    /// <param name="context">The context<see cref="ActionExecutingContext" />.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var response = new
            {
                MessagesSummary = new MessagesSummary(),
            };

            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage)
                .ToList();

            foreach (var error in errors)
            {
                response.MessagesSummary.Messages.Add(
                    new Message
                    {
                        MessageIndicatorType = MessageIndicatorTypes.Error.ToString(),
                        Text = error,
                        Title = ExceptionTypes.Validation.GetDescription(),
                    });
            }

            var messages = response?.MessagesSummary?.Messages;
            if (messages != null && messages.Any() && messages.Any(
                x => x.MessageIndicatorType == MessageIndicatorTypes.Error.ToString()))
            {
                throw new AppException(response!.MessagesSummary);
            }
        }

        base.OnActionExecuting(context);
    }
}