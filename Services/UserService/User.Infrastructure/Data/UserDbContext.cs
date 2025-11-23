namespace User.Infrastructure.Data;

/// <summary>
/// Defines the <see cref="UserDbContext" />.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserDbContext"/> class.
/// </remarks>
/// <param name="options">The options<see cref="DbContextOptions{UserDbContext}"/>.</param>
/// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
[ExcludeFromCodeCoverage(Justification = "EF DB context")]
public class UserDbContext(DbContextOptions<UserDbContext> options, IHttpContextAccessor httpContextAccessor)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IUserDbContext
{
    /// <summary>
    /// Gets the HttpContextAccessor.
    /// </summary>
    protected IHttpContextAccessor HttpContextAccessor { get; } = httpContextAccessor;

    /// <summary>
    /// Gets the Database.
    /// </summary>
    DatabaseFacade IUserDbContext.Database => this.Database;

    /// <summary>
    /// Gets or sets the SeedingEntries.
    /// </summary>
    public DbSet<SeedingEntry>? SeedingEntries { get; set; }

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
    /// <param name="builder">The modelBuilder<see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}

/// <summary>
/// Defines the <see cref="UserDbContextFactory" />.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "EF DB context")]
public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<UserDbContext>();
        var connectionString = configuration.GetConnectionString("defaultConnectionString");

        builder.UseSqlServer(connectionString);

        return new UserDbContext(builder.Options, null!);
    }
}