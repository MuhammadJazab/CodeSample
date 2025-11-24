namespace User.Infrastructure.Extensions;

/// <summary>
/// Defines the <see cref="InfrastructureSeedingExtension" />.
/// </summary>
public static class InfrastructureSeedingExtension
{
    /// <summary>
    /// Defines AddUserDbContext
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public static void AddUserDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
           x =>
           {
               x.MigrationsHistoryTable("__EFMigrationsHistory", "user");
               x.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName);
           }));
    }

    /// <summary>
    /// The Seed ERP Data.
    /// </summary>
    /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
    public static void SeedUserApiData(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<UserDbContext>();
        context?.Database.Migrate();

        var assembly = typeof(InfrastructureSeedingExtension).Assembly;
        var files = assembly.GetManifestResourceNames();

        var executedSeedings = context?.SeedingEntries?.ToArray();
        var filePrefix = $"{assembly.GetName().Name}.Seedings.";

        foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                    .Select(f => new
                    {
                        PhysicalFile = f,
                        LogicalFile = f.Replace(filePrefix, String.Empty)
                    })
                    .OrderBy(f => f.LogicalFile))
        {
            if (executedSeedings?.Any(e => e.Name == file.LogicalFile) ?? false)
                continue;

            string command;
            using (var stream = assembly?.GetManifestResourceStream(file.PhysicalFile))
            {
                using StreamReader reader = new(stream!);
                command = reader.ReadToEnd();
            }

            if (String.IsNullOrWhiteSpace(command))
                continue;

            using var transaction = context?.Database.BeginTransaction();
            try
            {
                context?.Database.ExecuteSqlRaw(command);
                context?.SeedingEntries?.Add(new SeedingEntry() { Name = file.LogicalFile });
                context?.SaveChanges();
                transaction?.Commit();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }
    }
}