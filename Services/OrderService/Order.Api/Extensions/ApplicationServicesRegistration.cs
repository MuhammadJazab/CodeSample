//file="ApplicationServicesRegistration.cs" >

using Order.Api.IntegrationEvents;

namespace Order.Api.Extensions;

/// <summary>
/// Defines the <see cref="ApplicationServicesRegistration" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ApplicationServicesRegistration
{
    /// <summary>
    /// Configure Application Services
    /// </summary>
    /// <param name="services"></param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddSingleton<IMessagePublisher, RabbitMQMessagePublisher>();
        return services;
    }
}