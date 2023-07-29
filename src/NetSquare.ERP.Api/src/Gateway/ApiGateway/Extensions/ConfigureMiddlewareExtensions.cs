//-----------------------------------------------------------------------
// <copyright file="ConfigureMiddlewareExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Extensions;

/// <summary>
/// Defines the <see cref="ConfigureMiddlewareExtensions" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigureMiddlewareExtensions
{
    /// <summary>
    /// The UseCustomMiddlewareConfiguration.
    /// </summary>
    /// <param name="app">The app<see cref="WebApplication"/>.</param>
    /// <param name="builder">The builder<see cref="WebApplicationBuilder"/>.</param>
    public static void UseCustomMiddlewareConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseHttpsRedirection();

        app.MapReverseProxy(options =>
        {
            options.UseLoadBalancing();
        });

        app.UseAuthorizationFilterMiddleware();

        app.Use(async (context, next) =>
        {
            context.Response.OnStarting(state =>
            {
                context.Response.Headers.Add(ReverseProxyConstants.AuthorizationKey, context.Request.Headers[ReverseProxyConstants.AuthorizationKey]);
                return Task.CompletedTask;
            }, context);

            await next();
        });

        app.Run();
    }
}