//-----------------------------------------------------------------------
// <copyright file="HttpContextExtensions.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Extentions;

/// <summary>
/// Defines the <see cref="HttpContextExtensions" />.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// The GeneratePolicyExceptions.
    /// </summary>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <param name="exceptionPolicies">The exceptionPolicies<see cref="IIndex{String, IExceptionHandler}" />.</param>
    /// <param name="exception">The exception<see cref="Exception" />.</param>
    /// <param name="isHandledException">The isHandledException<see cref="bool" />.</param>
    /// <param name="genericMessage">The genericMessage<see cref="string" />.</param>
    /// <returns>The <see cref="MessagesSummary"/>.</returns>
    public static MessagesSummary GeneratePolicyExceptions(
    this HttpContext httpContext,
    IIndex<string, IExceptionHandler> exceptionPolicies,
    Exception exception,
    bool isHandledException,
    string genericMessage)
    {
        IExceptionHandler exceptionPolicy;
        if (httpContext.User != null)
        {
            var userClaims = httpContext.User.Claims;
            if (userClaims.Any())
            {
                exceptionPolicy =
                    exceptionPolicies[userClaims.FirstOrDefault(x => x.Type.EndsWith("/role"))?.Value];
                return exceptionPolicy?.ExecutePolicy(exception, httpContext, isHandledException, genericMessage);
            }

            exceptionPolicy = exceptionPolicies[ExceptionPolicies.Client.ToString()];
            return exceptionPolicy?.ExecutePolicy(exception, httpContext, isHandledException, genericMessage);
        }

        return new MessagesSummary();
    }
}