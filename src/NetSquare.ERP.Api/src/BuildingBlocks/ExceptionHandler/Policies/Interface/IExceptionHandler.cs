//-----------------------------------------------------------------------
// <copyright file="IExceptionHandler.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Policies.Interface;

/// <summary>
/// Defines the <see cref="IExceptionHandler" />.
/// </summary>
public interface IExceptionHandler
{
    /// <summary>
    /// The ExecutePolicy.
    /// </summary>
    /// <param name="ex">The ex<see cref="Exception" />.</param>
    /// <param name="httpContext">The httpContext<see cref="HttpContext" />.</param>
    /// <param name="isHandledException">The isHandledException<see cref="bool" />.</param>
    /// <param name="genericMessage">The genericMessage<see cref="string" />.</param>
    /// <returns>The <see cref="MessagesSummary" />.</returns>
    MessagesSummary ExecutePolicy(
    Exception ex,
    HttpContext httpContext,
    bool isHandledException,
    string genericMessage);
}