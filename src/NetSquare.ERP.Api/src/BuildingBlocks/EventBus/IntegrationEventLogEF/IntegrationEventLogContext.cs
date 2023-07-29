//-----------------------------------------------------------------------
// <copyright file="IntegrationEventLogContext.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.IntegrationEventLogEF;

/// <summary>
/// Defines the <see cref="IntegrationEventLogContext" />.
/// </summary>
public class IntegrationEventLogContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationEventLogContext"/> class.
    /// </summary>
    /// <param name="options">The options<see cref="DbContextOptions{IntegrationEventLogContext}"/>.</param>
    public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the IntegrationEventLogs.
    /// </summary>
    public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

    /// <summary>
    /// The OnModelCreating.
    /// </summary>
    /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
    }

    /// <summary>
    /// The ConfigureIntegrationEventLogEntry.
    /// </summary>
    /// <param name="builder">The builder<see cref="EntityTypeBuilder{IntegrationEventLogEntry}"/>.</param>
    public static void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
    {
        builder.ToTable("IntegrationEventLog");

        builder.HasKey(e => e.EventId);

        builder.Property(e => e.EventId)
            .IsRequired();

        builder.Property(e => e.Content)
            .IsRequired();

        builder.Property(e => e.CreationTime)
            .IsRequired();

        builder.Property(e => e.State)
            .IsRequired();

        builder.Property(e => e.TimesSent)
            .IsRequired();

        builder.Property(e => e.EventTypeName)
            .IsRequired();
    }
}

