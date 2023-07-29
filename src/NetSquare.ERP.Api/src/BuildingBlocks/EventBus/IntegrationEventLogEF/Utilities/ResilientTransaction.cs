//-----------------------------------------------------------------------
// <copyright file="ResilientTransaction.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.IntegrationEventLogEF.Utilities;

/// <summary>
/// Defines the <see cref="ResilientTransaction" />.
/// </summary>
public class ResilientTransaction
{
    /// <summary>
    /// Defines the _context.
    /// </summary>
    private readonly DbContext _context;

    /// <summary>
    /// Prevents a default instance of the <see cref="ResilientTransaction"/> class from being created.
    /// </summary>
    /// <param name="context">The context<see cref="DbContext"/>.</param>
    private ResilientTransaction(DbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// The New.
    /// </summary>
    /// <param name="context">The context<see cref="DbContext"/>.</param>
    /// <returns>The <see cref="ResilientTransaction"/>.</returns>
    public static ResilientTransaction New(DbContext context) => new(context);

    /// <summary>
    /// The ExecuteAsync.
    /// </summary>
    /// <param name="action">The action<see cref="Func{Task}"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public async Task ExecuteAsync(Func<Task> action)
    {
        //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            await action();
            await transaction.CommitAsync();
        });
    }
}