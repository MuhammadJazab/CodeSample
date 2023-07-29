//-----------------------------------------------------------------------
// <copyright file="ApplicationRole.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Identity;

 /// <summary>
 /// Defines the <see cref="ApplicationRole" />.
 /// </summary>
public class ApplicationRole : IdentityRole<Guid>
{
    /// <summary>
    /// Gets or sets the CreatedOn.
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the CreatedOn.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the CreatedOn.
    /// </summary>
    public Guid UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the CreatedOn.
    /// </summary>
    public DateTime UpdatedOn { get; set; }
}