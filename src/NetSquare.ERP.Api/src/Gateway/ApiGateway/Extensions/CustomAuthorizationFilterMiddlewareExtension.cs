//-----------------------------------------------------------------------
// <copyright file="CustomAuthorizationFilterMiddlewareExtension.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Extensions;

/// <summary>
/// Defines the <see cref="CustomAuthorizationFilterMiddlewareExtension" />.
/// </summary>
public static class CustomAuthorizationFilterMiddlewareExtension
{
    /// <summary>
    /// The Definition of UseAuthorizationFilterMiddleware.
    /// </summary>
    /// <param name="builder">The builder<see cref="IApplicationBuilder"/>.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseAuthorizationFilterMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CustomAuthorizationFilterMiddleware>();

        return builder;
    }
}