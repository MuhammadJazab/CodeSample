//file="OrderConfiguration.cs" >

namespace Order.Infrastructure.Configurations;

/// <summary>
/// Defines the <see cref="OrderConfiguration" />.
/// </summary>    
[ExcludeFromCodeCoverage(Justification = "EF configuation file")]
public class OrderConfiguration
{
    /// <summary>
    /// Order Configure
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Order");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName(nameof(OrderEntity.Id))
            .HasColumnType("uniqueidentifier")
            .HasColumnOrder(0)
            .IsRequired();

        builder.Property(p => p.CustomerId)
            .HasColumnName(nameof(OrderEntity.CustomerId))
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(p => p.ProductId)
            .HasColumnName(nameof(OrderEntity.ProductId))
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(p => p.Address)
            .HasColumnName(nameof(OrderEntity.Address))
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName(nameof(OrderEntity.Description))
            .HasColumnType("nvarchar")
            .HasMaxLength(3500);

        builder.Property(p => p.OrderDate)
            .HasColumnName(nameof(OrderEntity.OrderDate))
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(p => p.CreatedOn)
            .HasColumnName(nameof(OrderEntity.CreatedOn))
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(p => p.UpdatedOn)
            .HasColumnName(nameof(OrderEntity.UpdatedOn))
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(p => p.CreatedByUserId)
            .HasColumnName(nameof(OrderEntity.CreatedByUserId))
            .HasColumnType("uniqueidentifier");

        builder.Property(p => p.UpdatedByUserId)
            .HasColumnName(nameof(OrderEntity.UpdatedByUserId))
            .HasColumnType("uniqueidentifier")
            .IsRequired(false);
    }
}
