//-----------------------------------------------------------------------
// <copyright file="ApplicationServicesRegistration.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Extensions;

/// <summary>
/// Defines the <see cref="ApplicationServicesRegistration" />.
/// </summary>
public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });

        return services;
    }
}