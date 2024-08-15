
//file="InfrastructureServicesRegistration.cs" >

namespace Order.Infrastructure.Extensions;

/// <summary>
/// Defines the <see cref="InfrastructureServicesRegistration" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class InfrastructureServicesRegistration
{
    /// <summary>
    /// The ConfigureInfrastructureServices.
    /// </summary> 
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("defaultConnectionString"),
                x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistoryOrder");
                    x.MigrationsAssembly(typeof(OrderDbContext).Assembly.GetName().Name);
                });
            });

        return services;
    }

    public static void SeedingOrderApiData(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<OrderDbContext>();
        context?.Database.Migrate();


        var assembly = typeof(OrderDbContext).Assembly;
        var files = assembly.GetManifestResourceNames();

        var executedSeedings = context?.SeedingEntries?.ToList()!;
        var filePrefix = $"{assembly.GetName().Name}.Seedings.";
        foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                .Select(
                    f => new
                    {
                        PhysicalFile = f,
                        LogicalFile = f.Replace(filePrefix, string.Empty),
                    })
                .OrderBy(f => f.LogicalFile))
        {
            if (executedSeedings.Exists(e => e.Name == file.LogicalFile))
            {
                continue;
            }

            var command = string.Empty;
            using (var stream = assembly.GetManifestResourceStream(file.PhysicalFile)!)
            {
                using StreamReader reader = new(stream);

                command = reader.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(command))
            {
                continue;
            }

            using var transaction = context?.Database.BeginTransaction();

            try
            {
                //context?.Database.ExecuteSqlRaw(command);
                //context?.SeedingEntries?.Add(new OrderSeedingEntry { Name = file.LogicalFile });
                //context?.SaveChanges();
                //transaction?.Commit();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }

    }
}

