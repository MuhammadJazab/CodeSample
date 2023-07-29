//-----------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Identity;

/// <summary>
/// Defines the <see cref="ApplicationUser" />.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public bool IsActive { get; set; }

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
    public Guid? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the CreatedOn.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }
}