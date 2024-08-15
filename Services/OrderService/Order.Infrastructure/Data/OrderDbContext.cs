//file="OrderDbContext.cs" >

namespace Order.Infrastructure.Data;

public class OrderDbContext : DbContext, IOrderDbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) :
        base(options)
    { }

    /// <summary>
    /// Gets the Database.
    /// </summary>
    DatabaseFacade IOrderDbContext.Database => this.Database;

    /// <summary>
    ///     Gets or sets the SeedingEntries.
    /// </summary>
    public virtual DbSet<OrderSeedingEntry>? SeedingEntries { get; set; }

    /// <summary>
    /// Gets or sets the SeedingEntries.
    /// </summary>
    public virtual DbSet<OrderEntity> Order { get; set; }

    /// <summary>
    /// The Set.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <returns>The <see cref="DbSet{T}"/>.</returns>
    public new DbSet<T> Set<T>() where T : BaseEntity
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
        this.ChangeTracker.DetectChanges();
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
            ////entry.Entity.UpdatedOn = DateTime.Now;
            ////entry.Entity.UpdatedByUserId = username;

            if (entry.State == EntityState.Added)
            {
                ////entry.Entity.CreatedOn = DateTime.Now;
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
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

    }
}

/// <summary>
/// Defines the <see cref="OrderDbContextFactory" />.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "EF DB context")]
public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    /// <summary>
    /// The CreateDbContext.
    /// </summary>
    /// <param name="args">The args<see cref="string[]"/>.</param>
    /// <returns>The <see cref="OrderDbContext"/>.</returns>
    public OrderDbContext CreateDbContext(string[] args)
    {

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<OrderDbContext>();
        var connectionString = configuration.GetConnectionString("defaultConnectionString");

        builder.UseNpgsql(connectionString);

        return new OrderDbContext(builder.Options);
    }
}