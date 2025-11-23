
namespace User.Infrastructure.SeedWork;

public interface IUserDbContext
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
