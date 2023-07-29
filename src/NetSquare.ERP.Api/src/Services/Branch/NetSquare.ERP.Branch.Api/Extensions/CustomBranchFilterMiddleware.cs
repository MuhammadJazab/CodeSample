//-----------------------------------------------------------------------
// <copyright file="CustomBranchFilterMiddleware.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Extensions;

/// <summary>
/// Defines the <see cref="CustomBranchFilterMiddleware" />.
/// </summary>
public class CustomBranchFilterMiddleware
{
    /// <summary>
    /// Defines the _next.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomBranchFilterMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next<see cref="RequestDelegate"/>.</param>
    public CustomBranchFilterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// The InvokeAsync.
    /// </summary>
    /// <param name="context">The context<see cref="HttpContext"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"]!;
        if (authHeader != null && authHeader.StartsWith("Bearer"))
        {
            // Extract the token from the header
            string token = authHeader["Bearer ".Length..].Trim();
            // Validate the token
            if (!await ValidateTokenAsync(token))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }
        await _next(context);
    }

    /// <summary>
    /// The ValidateTokenAsync.
    /// </summary>
    /// <param name="token">The token<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{bool}"/>.</returns>
    private static Task<bool> ValidateTokenAsync(string token)
    {
        // Will add our logic to validate the token
        if (token is null)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}