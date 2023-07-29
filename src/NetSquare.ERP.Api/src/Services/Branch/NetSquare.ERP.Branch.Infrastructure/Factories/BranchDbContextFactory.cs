//-----------------------------------------------------------------------
// <copyright file="BranchDbContextFactory.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Infrastructure.Factories;

/// <summary>
/// Defines the <see cref="BranchDbContextFactory" />.
/// </summary>
public class BranchDbContextFactory : IDesignTimeDbContextFactory<BranchDbContext>
{
    public BranchDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<BranchDbContext>();
        var connectionString = configuration.GetConnectionString("defaultConnectionString");

        builder.UseSqlServer(connectionString);

        return new BranchDbContext(builder.Options, null!);
    }
}