//-----------------------------------------------------------------------
// <copyright file="IBranchRepository.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Domain.SeedWork;

/// <summary>
/// Defines the <see cref="IBranchRepository" />.
/// </summary>
/// <typeparam name="T">.</typeparam>
public interface IBranchRepository : IGenericRepository<ERP.Branch.Domain.Entities.Branch>
{

}