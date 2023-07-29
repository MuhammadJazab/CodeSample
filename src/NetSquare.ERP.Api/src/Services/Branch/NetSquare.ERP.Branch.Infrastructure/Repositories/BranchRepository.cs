//-----------------------------------------------------------------------
// <copyright file="BranchRepository.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Infrastructure.Repositories;

/// <summary>
/// Defines the <see cref="BranchRepository" />.
/// </summary>
public class BranchRepository : GenericRepository<Branch.Domain.Entities.Branch>, IBranchRepository
{
    /// <summary>
    /// Branch db context
    /// </summary>
    private IBranchDbContext branchDbContext { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BranchRepository"/> class.
    /// </summary>
    /// <param name="branchDbContext"><see cref="IBranchDbContext"/></param>
    public BranchRepository(IBranchDbContext branchDbContext) : base(branchDbContext)
    {
        this.branchDbContext = branchDbContext;
    }
}

