//-----------------------------------------------------------------------
// <copyright file="ILoginService.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Domain.SeedWork;

/// <summary>
/// Defines the <see cref="ILoginService{T}" />.
/// </summary>
/// <typeparam name="T">.</typeparam>
public interface IHumanResourceService<T>
{
    /// <summary>
    /// The GetAllHumanResource.
    /// </summary>
    /// <returns>The <see cref="List{T}"/>.</returns>
    Task<List<T>> GetAllResource();
}