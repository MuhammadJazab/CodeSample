//-----------------------------------------------------------------------
// <copyright file="IGenericRepository.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Domain.SeedWork;

/// <summary>
/// Defines the <see cref="IGenericRepository{T}" />.
/// </summary>
/// <typeparam name="T">.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// The Table.
    /// </summary>
    /// <param name="predicate">The predicate<see cref="Expression{Func{T, bool}}"/>.</param>
    /// <returns>The <see cref="Task{IQueryable{T}}"/>.</returns>
    Task<IQueryable<T>> Table(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// The ToListAsync.
    /// </summary>
    /// <returns>The <see cref="Task{List{T}}"/>.</returns>
    Task<List<T>> ToListAsync();

    /// <summary>
    /// The GetById.
    /// </summary>
    /// <param name="id">The id<see cref="object"/>.</param>
    /// <returns>The <see cref="Task{T?}"/>.</returns>
    Task<T?> GetById(object id);

    /// <summary>
    /// The Insert.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task Insert(T entity);

    /// <summary>
    /// The Update.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task Update(T entity);

    /// <summary>
    /// The Delete.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task Delete(T entity);

    /// <summary>
    /// The SaveChangesAsync.
    /// </summary>
    /// <returns>The <see cref="Task{int}"/>.</returns>
    Task<int> SaveChangesAsync();
}