//-----------------------------------------------------------------------
// <copyright file="HumanResourceDbContextFactory.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Infrastructure.Factories;

/// <summary>
/// Defines the <see cref="HumanResourceDbContextFactory" />.
/// </summary>
public class HumanResourceDbContextFactory : IDesignTimeDbContextFactory<HumanResourceDbContext>
{
    public HumanResourceDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<HumanResourceDbContext>();
        var connectionString = configuration.GetConnectionString("defaultConnectionString");

        builder.UseSqlServer(connectionString);

        return new HumanResourceDbContext(builder.Options, null!);
    }
}