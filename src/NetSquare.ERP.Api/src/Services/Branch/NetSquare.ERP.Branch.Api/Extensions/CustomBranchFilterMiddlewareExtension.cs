//-----------------------------------------------------------------------
// <copyright file="ConfigureAutoFacContainerExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Extensions;

/// <summary>
/// Defines the <see cref="CustomBranchFilterMiddlewareExtension" />.
/// </summary>
public static class CustomBranchFilterMiddlewareExtension
{
    /// <summary>
    /// The UseGroupCapacityFilterMiddleware.
    /// </summary>
    /// <param name="builder">The builder<see cref="IApplicationBuilder"/>.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseGroupCapacityFilterMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomBranchFilterMiddleware>();
    }
}