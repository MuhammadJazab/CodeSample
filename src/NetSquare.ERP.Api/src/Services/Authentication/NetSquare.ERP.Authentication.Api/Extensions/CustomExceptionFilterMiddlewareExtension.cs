//-----------------------------------------------------------------------
// <copyright file="ConfigureAutoFacContainerExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Extensions;

/// <summary>
/// Defines the <see cref="CustomExceptionFilterMiddlewareExtension" />.
/// </summary>
public static class CustomExceptionFilterMiddlewareExtension
{
    /// <summary>
    /// The UseExceptionFilterMiddleware.
    /// </summary>
    /// <param name="builder">The builder<see cref="IApplicationBuilder"/>.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseExceptionFilterMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionFilterMiddleware>();
    }
}

