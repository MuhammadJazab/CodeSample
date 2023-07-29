//-----------------------------------------------------------------------
// <copyright file="IAuthenticationDbContext.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.SeedWork;

/// <summary>
/// Defines the <see cref="IAuthenticationDbContext" />.
/// </summary>
public interface IAuthenticationDbContext
{
    /// <summary>
    /// The Set.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="DbSet{T}"/>.</returns>
    DbSet<T> Set<T>() where T : BaseEntity;

    /// <summary>
    /// The SaveChangesAsync.
    /// </summary>
    /// <returns>The <see cref="Task{int}"/>.</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// The Entry.
    /// </summary>
    /// <param name="entity">The entity<see cref="object"/>.</param>
    /// <returns>The <see cref="EntityEntry"/>.</returns>
    EntityEntry Entry(object entity);

    /// <summary>
    /// Gets the Database.
    /// </summary>
    DatabaseFacade Database { get; }
}