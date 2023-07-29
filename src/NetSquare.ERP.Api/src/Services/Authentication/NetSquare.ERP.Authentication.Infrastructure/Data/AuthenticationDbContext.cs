//-----------------------------------------------------------------------
// <copyright file="AuthenticationDbContext.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Infrastructure.Data;

/// <summary>
/// Defines the <see cref="AuthenticationDbContext" />.
/// </summary>
public class AuthenticationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IAuthenticationDbContext
{
    /// <summary>
    /// Gets the HttpContextAccessor.
    /// </summary>
    protected IHttpContextAccessor HttpContextAccessor { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options<see cref="DbContextOptions{AuthenticationDbContext}"/>.</param>
    /// <param name="httpContextAccessor">The httpContextAccessor<see cref="IHttpContextAccessor"/>.</param>
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options) => this.HttpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Gets the Database.
    /// </summary>
    DatabaseFacade IAuthenticationDbContext.Database => this.Database;

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
    /// The Update.
    /// </summary>
    /// <param name="entity">The entity<see cref="object"/>.</param>
    /// <returns>The <see cref="Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry"/>.</returns>
    public override Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry Update(object entity)
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
    /// <param name="builder">The modelBuilder<see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("erp");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}

public class AuthenticationDbContextFactory : IDesignTimeDbContextFactory<AuthenticationDbContext>
{
    public AuthenticationDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<AuthenticationDbContext>();
        var connectionString = configuration.GetConnectionString("defaultConnectionString");

        builder.UseSqlServer(connectionString);

        return new AuthenticationDbContext(builder.Options, null!);
    }
}