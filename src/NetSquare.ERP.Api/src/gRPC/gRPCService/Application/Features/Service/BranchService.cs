//-----------------------------------------------------------------------
// <copyright file="BranchService.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace gRPCService.Application.Features.Service;

/// <summary>
/// Defines the <see cref="BranchService" />.
/// </summary>
public class BranchService : BranchGrpc.BranchGrpcBase
{
    /// <summary>
    /// The mediator
    /// </summary>
    private readonly IMediator mediator;

    /// <summary>
    ///  Initializes a new instance of the <see cref="BranchService"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public BranchService(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Get users list for gRPC request
    /// </summary>
    /// <param name="request"><see cref="AddUserToBranchRequest"/></param>
    /// <param name="context"><see cref="ServerCallContext"/></param>
    /// <returns></returns>
    public override Task<AddUserToBranchResponse> AddUserToBranch(AddUserToBranchRequest request, ServerCallContext context)
    {
        return base.AddUserToBranch(request, context);
    }
}

