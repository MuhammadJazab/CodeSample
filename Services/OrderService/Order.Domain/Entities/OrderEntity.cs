//file="OrderEntity.cs" >

namespace Order.Domain.Entities;

/// <summary>
/// Defines the <see cref="OrderEntity" />.
/// </summary>
public class OrderEntity : BaseAuditable
{
    [Key]
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductId { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.MinValue;
}
