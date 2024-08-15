//file="ApplicationServicesRegistration.cs" >

namespace User.Api.Application.Extensions;

/// <summary>
/// Defines the <see cref="ApplicationServicesRegistration" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ApplicationServicesRegistration
{
    /// <summary>
    /// The ConfigureApplicationServices.
    /// </summary>
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        return services;
    }
}

