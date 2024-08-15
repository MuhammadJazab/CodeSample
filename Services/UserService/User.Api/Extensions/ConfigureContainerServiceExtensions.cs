//file="ConfigureContainerServiceExtensions.cs" >

namespace User.Api.Extensions;

/// <summary>
/// Defines the <see cref="ConfigureContainerServiceExtensions" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigureContainerServiceExtensions
{
    /// <summary>
    /// The ConfigureAutoFacContainer.
    /// </summary>
    /// <param name="builder">The builder<see cref="WebApplicationBuilder"/>.</param>
    public static void ConfigureAutoFacContainer(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterServices()));
    }
}