//-----------------------------------------------------------------------
// <copyright file="InfrastructureSeedingExtension.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Infrastructure.Extensions;

/// <summary>
/// Defines the <see cref="DbContextExtensions" />.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Defines AddBranchApiDbContext
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public static void AddBranchApiDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BranchDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("defaultConnectionString"),
                        x =>
                        {
                            x.MigrationsHistoryTable("__EFMigrationsHistoryBranch", "erp");
                            x.MigrationsAssembly(typeof(BranchDbContext).Assembly.GetName().Name);
                        });
                });
    }

    /// <summary>
    /// The Seed ERP Data.
    /// </summary>
    /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
    public static void SeedBranchApiData(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<BranchDbContext>();
        context?.Database.Migrate();

        var assembly = typeof(BranchDbContext).Assembly;
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

            string command = string.Empty;
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
                context?.SeedingEntries?.Add(new BranchSeedingEntry() { Name = file.LogicalFile });
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

