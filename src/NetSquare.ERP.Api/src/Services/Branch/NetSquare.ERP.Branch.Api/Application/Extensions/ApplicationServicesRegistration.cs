//-----------------------------------------------------------------------
// <copyright file="ApplicationServicesRegistration.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Application.Extensions;

/// <summary>
/// Defines the <see cref="ApplicationServicesRegistration" />.
/// </summary>
public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}