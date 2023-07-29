//-----------------------------------------------------------------------
// <copyright file="UserLoginRequestQuery.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Features.Account.Requests.Queries;

/// <summary>
/// Defines the <see cref="GetUsersRequestQuery" />.
/// </summary>
public class GetUsersRequestQuery : IRequest<List<GetUsersResponse>>
{
	public GetUsersRequestQuery()
	{
	}
}