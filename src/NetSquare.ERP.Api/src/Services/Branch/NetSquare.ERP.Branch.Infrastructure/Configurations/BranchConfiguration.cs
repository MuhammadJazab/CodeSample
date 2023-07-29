//-----------------------------------------------------------------------
// <copyright file="SeedingEntryConfiguration.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Infrastructure.Configurations;

/// <summary>
/// Defines the <see cref="BranchConfiguration" />.
/// </summary>
public class BranchConfiguration : IEntityTypeConfiguration<Branch.Domain.Entities.Branch>
{
    /// <summary>
    /// The Configure.
    /// </summary>
    /// <param name="builder">The builder<see cref="EntityTypeBuilder{SeedingEntry}"/>.</param>
    public void Configure(EntityTypeBuilder<Branch.Domain.Entities.Branch> builder)
    {
        builder.ToTable(nameof(Branch.Domain.Entities.Branch));
        builder.HasKey(b => b.BranchId);

        builder.Property(b => b.BranchId)
            .HasColumnName(nameof(Branch.Domain.Entities.Branch.BranchId))
            .HasColumnType("uniqueidentifier")
            .HasColumnOrder(0)
            .IsRequired();

        builder.Property(b => b.BranchName)
            .HasColumnName(nameof(Branch.Domain.Entities.Branch.BranchName))
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.BranchAddress)
           .HasColumnName(nameof(Branch.Domain.Entities.Branch.BranchAddress))
           .HasColumnType("nvarchar")
           .HasMaxLength(300)
           .IsRequired();

        builder.Property(b => b.BranchCity)
            .HasColumnName(nameof(Branch.Domain.Entities.Branch.BranchCity))
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(b => b.BranchManager)
            .HasColumnName(nameof(Branch.Domain.Entities.Branch.BranchManager))
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(p => p.CreatedOn)
            .HasColumnName(nameof(Branch.Domain.Entities.Branch.CreatedOn))
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(p => p.CreatedBy)
            .HasColumnName(nameof(Branch.Domain.Entities.Branch.CreatedBy))
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(p => p.UpdatedOn)
           .HasColumnName(nameof(Branch.Domain.Entities.Branch.UpdatedOn))
           .HasColumnType("datetime")
           .IsRequired(false);

        builder.Property(p => p.UpdatedBy)
            .HasColumnName(nameof(Branch.Domain.Entities.Branch.UpdatedBy))
            .HasColumnType("uniqueidentifier")
            .IsRequired(false);
    }
}

