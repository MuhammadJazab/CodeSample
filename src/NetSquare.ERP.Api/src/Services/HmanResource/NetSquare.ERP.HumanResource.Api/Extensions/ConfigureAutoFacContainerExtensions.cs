//-----------------------------------------------------------------------
// <copyright file="ConfigureAutoFacContainerExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Api.Extensions;

/// <summary>
/// Defines the <see cref="ConfigureAutoFacContainerExtensions" />.
/// </summary>
public static class ConfigureAutoFacContainerExtensions
{
    /// <summary>
    /// The ConfigureAutoFacContainer.
    /// </summary>
    /// <param name="builder">The builder<see cref="WebApplicationBuilder"/>.</param>
    public static void ConfigureAutoFacContainer(this WebApplicationBuilder appBuilder)
    {
        appBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        appBuilder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterType<HumanResourceDbContext>().As<IHumanResourceDbContext>().InstancePerLifetimeScope());

        appBuilder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterRepositories()));
        appBuilder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterServices()));

        appBuilder.Services.AddHttpContextAccessor();
        appBuilder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
} 