
//file="BaseAuditable.cs" >



namespace Order.Domain.Common;

/// <summary>
/// Defines the <see cref="BaseAuditable" />.
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class BaseAuditable : BaseEntity
{
    /// <summary>
    /// Gets or sets the CreatedOn.
    /// </summary>
    public DateTime? CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the UpdatedOn.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }

    /// <summary>
    /// Gets or sets the CreatedByUserId.
    /// </summary>
    public Guid CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the UpdatedByUserId.
    /// </summary>
    public Guid? UpdatedByUserId { get; set; }
}

