//-----------------------------------------------------------------------
// <copyright file="Branch.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Domain.Entities;

/// <summary>
/// Defines the <see cref="Branch" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class Branch : BaseAuditable
{
    /// <summary>
    /// Gets or sets the BranchId
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the BranchName
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// Gets or sets the BranchAddress
    /// </summary>
    public string BranchAddress { get; set; }

    /// <summary>
    /// Gets or sets the BranchCity
    /// </summary>
    public Guid BranchCity { get; set; }

    /// <summary>
    /// Gets or sets the BranchManager
    /// </summary>
    public Guid BranchManager { get; set; }
}