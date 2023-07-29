//-----------------------------------------------------------------------
// <copyright file="HumanResourceDbContext.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Infrastructure.Data;

/// <summary>
/// Defines the <see cref="HumanResourceDbContext" />.
/// </summary>
public class HumanResourceDbContext : DbContext, IHumanResourceDbContext
{
    /// <summary>
    /// Gets the HttpContextAccessor.
    /// </summary>
    protected IHttpContextAccessor HttpContextAccessor { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HumanResourceDbContext"/> class.
    /// </summary>
    /// <param name="options">The options<see cref="DbContextOptions{HumanResourceDbContext}"/>.</param>
    /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
    public HumanResourceDbContext(DbContextOptions<HumanResourceDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options) => this.HttpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Gets the Database.
    /// </summary>
    DatabaseFacade Database => this.Database;

    /// <summary>
    /// Gets or sets the SeedingEntries.
    /// </summary>
    public DbSet<HumanResourceSeedingEntry>? SeedingEntries { get; set; }

    /// <summary>
    /// The Set.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="DbSet{T}"/>.</returns>
    public new DbSet<T> Set<T>()
        where T : BaseEntity
    {
        return base.Set<T>();
    }

    /// <summary>
    /// The Update.
    /// </summary>
    /// <param name="entity">The entity<see cref="object"/>.</param>
    /// <returns>The <see cref="EntityEntry"/>.</returns>
    public override EntityEntry Update(object entity)
    {
        return base.Update(entity);
    }

    /// <summary>
    /// The SaveChangesAsync.
    /// </summary>
    /// <returns>The <see cref="Task{int}"/>.</returns>
    public async Task<int> SaveChangesAsync()
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseAuditable>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.UpdatedOn = DateTime.Now;
            ////entry.Entity.UpdatedByUserId = username;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.Now;
                ////entry.Entity.CreatedByUserId = username;
            }
        }

        return await base.SaveChangesAsync();
    }

    /// <summary>
    /// The OnModelCreating.
    /// </summary>
    /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("erp");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}