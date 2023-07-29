//-----------------------------------------------------------------------
// <copyright file="GetBranchesRequestQueryHandler.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Application.Features.Branch.Handlers.Queries;

/// <summary>
/// Defines the <see cref="GetBranchesRequestQueryHandler" />.
/// </summary>
public class GetBranchesRequestQueryHandler : IRequestHandler<GetBranchesRequestQuery, List<GetBranchesResponse>>
{
    /// <summary>
    /// Defines the loginService.
    /// </summary>
    private readonly IBranchRepository branchService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchesRequestQueryHandler"/> class.
    /// </summary>
    /// <param name="branchService">The loginService<see cref="IBranchRepository"/>.</param>
    public GetBranchesRequestQueryHandler(IBranchRepository branchService)
    {
        this.branchService = branchService;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request"><see cref="GetBranchesRequestQuery"/></param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns><see cref="List{GetBranchesResponse}"/></returns>
    public async Task<List<GetBranchesResponse>> Handle(GetBranchesRequestQuery request, CancellationToken cancellationToken)
    {
        var branches = await this.branchService.ToListAsync();

        return branches.Select(branch => new GetBranchesResponse(branch)).ToList();
    }
}