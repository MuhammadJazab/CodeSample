namespace User.Infrastructure.Configurations;

/// <summary>
/// Defines the <see cref="SeedingEntryConfiguration" />.
/// </summary>
public class SeedingEntryConfiguration : IEntityTypeConfiguration<SeedingEntry>
{
    /// <summary>
    /// The Configure.
    /// </summary>
    /// <param name="builder">The builder<see cref="EntityTypeBuilder{SeedingEntry}"/>.</param>
    public void Configure(EntityTypeBuilder<SeedingEntry> builder)
    {
        builder.ToTable("__UserSeedingHistory");
        builder.HasKey(s => s.Name);
    }
}