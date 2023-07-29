//-----------------------------------------------------------------------
// <copyright file="GetBranchesRequestQuery.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Application.Features.Branch.Requests.Queries;

/// <summary>
/// Defines the <see cref="GetBranchesRequestQuery" />.
/// </summary>
public class GetBranchesRequestQuery : IRequest<List<GetBranchesResponse>>
{
	public GetBranchesRequestQuery()
	{
	}
}