//file="OrderSeedingEntryConfiguration.cs" >

namespace Order.Infrastructure.Configurations;

public class OrderSeedingEntryConfiguration : IEntityTypeConfiguration<OrderSeedingEntry>
{
    /// <summary>
    ///     The Configure.
    /// </summary>
    /// <param name="builder">The builder<see cref="EntityTypeBuilder{SeedingEntry}" />.</param>
    public void Configure(EntityTypeBuilder<OrderSeedingEntry> builder)
    {
        builder.ToTable("__OrderSeedingHistory");
        builder.HasKey(s => s.Name);
    }
}
