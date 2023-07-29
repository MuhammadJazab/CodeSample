//-----------------------------------------------------------------------
// <copyright file="ConfigureContainerServiceExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Extensions;

/// <summary>
/// Defines the <see cref="ConfigureContainerServiceExtensions" />.
/// </summary>
public static class ConfigureContainerServiceExtensions
{
    /// <summary>
    /// The ConfigureAutoFacContainer.
    /// </summary>
    /// <param name="builder">The builder<see cref="WebApplicationBuilder"/>.</param>
    public static void ConfigureAutoFacContainer(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterType<AuthenticationDbContext>().As<IAuthenticationDbContext>().InstancePerLifetimeScope());

        builder.Services.Configure<GrpcConfiguration>(builder.Configuration.GetSection(nameof(GrpcConfiguration)));

        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterRepositories()));
        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterServices()));
    }
}