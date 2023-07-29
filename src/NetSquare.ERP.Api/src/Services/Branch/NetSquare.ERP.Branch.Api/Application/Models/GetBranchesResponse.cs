//-----------------------------------------------------------------------
// <copyright file="GetBranchesResponse.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Application.Models;

/// <summary>
/// Defines the <see cref="GetBranchesResponse" />.
/// </summary>
public class GetBranchesResponse : BaseResponse
{
    /// <summary>
    /// Get of set the Branch Id
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Get of set the Branch Name
    /// </summary>
	public string BranchName { get; set; }

    /// <summary>
    /// Get of set the Manager of Branch
    /// </summary>
	public Guid BranchManager { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchesResponse"/> class.
    /// </summary>
    /// <param name="branch"><see cref="Branch.Domain.Entities.Branch"/></param>
    public GetBranchesResponse(Branch.Domain.Entities.Branch branch)
	{
		this.BranchId = branch.BranchId;
		this.BranchName = branch.BranchName;
		this.BranchManager = branch.BranchManager;
	}
}