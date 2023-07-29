//-----------------------------------------------------------------------
// <copyright file="GenericRepository.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Infrastructure.Repositories;

/// <summary>
/// Defines the <see cref="GenericRepository{T}" />.
/// </summary>
/// <typeparam name="T">.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Defines the Entities.
    /// </summary>
    protected readonly DbSet<T> Entities;

    /// <summary>
    /// Defines the context.
    /// </summary>
    private readonly IBranchDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
    /// </summary>
    /// <param name="context">The context<see cref="IBranchDbContext"/>.</param>
    public GenericRepository(IBranchDbContext context)
    {
        this.context = context;
        this.Entities = context.Set<T>();
    }

    /// <summary>
    /// The Table.
    /// </summary>
    /// <param name="predicate">The predicate<see cref="Expression{Func{T, bool}}"/>.</param>
    /// <returns>The <see cref="Task{IQueryable{T}}"/>.</returns>
    public Task<IQueryable<T>> Table(Expression<Func<T, bool>> predicate) => Task.FromResult(this.Entities.Where(predicate).AsQueryable());

    /// <summary>
    /// The ToListAsync.
    /// </summary>
    /// <returns>The <see cref="Task{List{T}}"/>.</returns>
    public async Task<List<T>> ToListAsync() => await this.Entities.ToListAsync();

    /// <summary>
    /// The GetById.
    /// </summary>
    /// <param name="id">The id<see cref="object"/>.</param>
    /// <returns>The <see cref="Task{T?}"/>.</returns>
    public async Task<T?> GetById(object id) => await Entities.FindAsync(id);

    /// <summary>
    /// The Insert.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(Convert.ToString(nameof(T)));
        }

        return this.InsertEntity(entity);
    }

    /// <summary>
    /// The InsertEntity.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task InsertEntity(T entity)
    {
        await this.Entities.AddAsync(entity);
        await this.SaveChangesAsync();
    }

    /// <summary>
    /// The SaveChangesAsync.
    /// </summary>
    /// <returns>The <see cref="Task{int}"/>.</returns>
    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// The Update.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(T).ToString());
        }

        return this.UpdateEntity(entity);
    }

    /// <summary>
    /// The UpdateEntity.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task UpdateEntity(T entity)
    {
        this.Entities.Attach(entity);
        this.context.Entry(entity).State = EntityState.Modified;
        await this.SaveChangesAsync();
        await Task.FromResult(default(T));
    }

    /// <summary>
    /// The Delete.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(T).ToString());
        }

        return this.DeleteEntity(entity);
    }

    /// <summary>
    /// The DeleteEntity.
    /// </summary>
    /// <param name="entity">The entity<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task DeleteEntity(T entity)
    {
        this.Entities.Attach(entity);
        this.context.Entry(entity).State = EntityState.Deleted;
        await this.SaveChangesAsync();
        await Task.FromResult(default(T));
    }
}