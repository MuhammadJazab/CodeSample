//file="ConfigureContainerServiceExtensions.cs" >

namespace Order.Api.Extensions;

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

        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterType<OrderDbContext>().As<IOrderDbContext>().InstancePerLifetimeScope());

        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterRepositories()));
        builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RegisterServices()));

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //builder.Services.AddTransient<IEmailService, EmailService>();

        // MassTransit-RabbitMQ Configuration
        //builder.Services.AddMassTransit(config => {

        //    config.AddConsumer<EmailConsumer>();

        //    config.UsingRabbitMq((ctx, cfg) => {
        //        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //        //cfg.UseHealthCheck(ctx);

        //        cfg.ReceiveEndpoint(EventBusConstants.RegisterationEmailQueue, c => {
        //            c.ConfigureConsumer<EmailConsumer>(ctx);
        //        });
        //    });
        //});
    }
}