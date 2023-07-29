//-----------------------------------------------------------------------
// <copyright file="CustomHumanResourceFilterMiddleware.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Api.Extensions;

/// <summary>
/// Defines the <see cref="CustomHumanResourceFilterMiddlewareExtension" />.
/// </summary>
public static class CustomHumanResourceFilterMiddlewareExtension
{
    /// <summary>
    /// The UseHumanResourceFilterMiddleware.
    /// </summary>
    /// <param name="builder">The builder<see cref="IApplicationBuilder"/>.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseHumanResourceFilterMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomHumanResourceFilterMiddleware>();
    }
}