//-----------------------------------------------------------------------
// <copyright file="CustomExceptionMiddlewareExtensions.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Middleware;

/// <summary>
/// Defines the <see cref="CustomExceptionMiddlewareExtensions" />.
/// </summary>
public static class CustomExceptionMiddlewareExtensions
{
    /// <summary>
    /// The ConfigureCustomExceptionMiddleware.
    /// </summary>
    /// <param name="app">The app<see cref="IApplicationBuilder" />.</param>
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionMiddleware>();
    }
}