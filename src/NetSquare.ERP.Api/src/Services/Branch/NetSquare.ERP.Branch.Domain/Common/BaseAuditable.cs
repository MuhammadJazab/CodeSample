//-----------------------------------------------------------------------
// <copyright file="BaseAuditable.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Domain.Common;

/// <summary>
/// Defines the <see cref="BaseAuditable" />.
/// </summary>
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
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the UpdatedByUserId.
    /// </summary>
    public Guid? UpdatedBy { get; set; }
}