//-----------------------------------------------------------------------
// <copyright file="SeedingEntryConfiguration.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Infrastructure.Configurations;

/// <summary>
/// Defines the <see cref="SeedingEntryConfiguration" />.
/// </summary>
public class SeedingEntryConfiguration : IEntityTypeConfiguration<BranchSeedingEntry>
{
    /// <summary>
    /// The Configure.
    /// </summary>
    /// <param name="builder">The builder<see cref="EntityTypeBuilder{SeedingEntry}"/>.</param>
    public void Configure(EntityTypeBuilder<BranchSeedingEntry> builder)
    {
        builder.ToTable("__BranchSeedingHistory");
        builder.HasKey(s => s.Name);
    }
}