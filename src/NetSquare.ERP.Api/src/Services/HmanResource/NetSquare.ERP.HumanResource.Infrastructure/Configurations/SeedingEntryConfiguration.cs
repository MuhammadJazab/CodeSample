//-----------------------------------------------------------------------
// <copyright file="SeedingEntryConfiguration.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Infrastructure.Configurations;

/// <summary>
/// Defines the <see cref="SeedingEntryConfiguration" />.
/// </summary>
public class SeedingEntryConfiguration : IEntityTypeConfiguration<HumanResourceSeedingEntry>
{
    /// <summary>
    /// The Configure.
    /// </summary>
    /// <param name="builder">The builder<see cref="EntityTypeBuilder{SeedingEntry}"/>.</param>
    public void Configure(EntityTypeBuilder<HumanResourceSeedingEntry> builder)
    {
        builder.ToTable("__HumanResourceSeedingHistory");
        builder.HasKey(s => s.Name);
    }
}