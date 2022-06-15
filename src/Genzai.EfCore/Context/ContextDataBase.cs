using Genzai.EfCore.Extensions;

namespace Genzai.EfCore.Context;

/// <summary>
/// Context Database.
/// </summary>
public abstract class ContextDataBase<TContext> : DbContext
    where TContext : DbContext
{
    /// <summary>
    /// Mediator service.
    /// </summary>
    private readonly IMediator mediator;

    private readonly ClaimsPrincipal claimsPrincipal;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextDataBase{TContext}"/> class.
    /// </summary>
    /// <param name="options">Context options.</param>
    /// <param name="mediator">Mediator service.</param>
    /// <param name="claimsPrincipal">Current principal user.</param>
#pragma warning disable CS8618

    protected ContextDataBase(DbContextOptions<TContext> options, IMediator mediator, ClaimsPrincipal claimsPrincipal)
#pragma warning restore CS8618
        : base(options)
    {
        this.mediator = mediator;
        this.claimsPrincipal = claimsPrincipal;
    }

    /// <summary>
    /// Current transaction.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public IDbContextTransaction CurrentTransaction { get; private set; }

    /// <summary>
    /// Has active transaction.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public bool HasActiveTransaction => this.CurrentTransaction != null;

    ///<inheritdoc/>
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (this.CurrentTransaction != null)
        {
#pragma warning disable CS8603
            return null;
#pragma warning restore CS8603
        }

        this.CurrentTransaction =
            await this.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        return this.CurrentTransaction;
    }

    ///<inheritdoc/>
    public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(transaction,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(transaction)));

        Guard.Against<InvalidOperationException>(transaction != this.CurrentTransaction,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.TransactionNotCurrent, transaction.TransactionId));

        try
        {
            await this.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync();
        }
        catch
        {
            this.RollbackTransaction();
            throw;
        }
        finally
        {
            if (this.CurrentTransaction != null)
            {
                this.CurrentTransaction.Dispose();
#pragma warning disable CS8625
                this.CurrentTransaction = null;
#pragma warning restore CS8625
            }
        }
    }

    ///<inheritdoc/>
    public int ExecuteSqlCommand(string sqlCommand, params object[] parameters)
    {
        Guard.IsNotNullNorEmpty(
            sqlCommand,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(sqlCommand)));
        Guard.IsNotNull(
            parameters,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(parameters)));

        return this.Database.ExecuteSqlRaw(sqlCommand, parameters);
    }

    ///<inheritdoc/>
    public async Task<int> ExecuteSqlCommandAsync(
        string sqlCommand, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullNorEmpty(
            sqlCommand,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(sqlCommand)));
        Guard.IsNotNull(
            parameters,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(parameters)));

        return await this.Database.ExecuteSqlRawAsync(sqlCommand, parameters, cancellationToken);
    }

    ///<inheritdoc/>
    public void RollbackTransaction()
    {
        try
        {
            this.CurrentTransaction?.Rollback();
        }
        finally
        {
            if (this.CurrentTransaction != null)
            {
                this.CurrentTransaction.Dispose();
#pragma warning disable CS8625
                this.CurrentTransaction = null;
#pragma warning restore CS8625
            }
        }
    }

    ///<inheritdoc/>
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection.
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions.
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.

        var result = await SaveChangesAsync(cancellationToken);

        await mediator.DispatchDomainEventsAsync(this, cancellationToken: cancellationToken).ConfigureAwait(false);

        return result > 0;
    }

    /// <summary>
    /// Save operation for Autidables Entities
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> SaveAuditChangesAsync(CancellationToken cancellationToken = default)
    {
        var insertedEntries = this.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity);

        foreach (var insertedEntry in insertedEntries)
        {
            //If the inserted object is an Auditable.
            if (insertedEntry is IAuditable auditableEntity)
            {
                auditableEntity.CreatedInformation(claimsPrincipal.Identity?.Name);
            }
        }

        var modifiedEntries = this.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var modifiedEntry in modifiedEntries)
        {
            //If the inserted object is an Auditable.
            if (modifiedEntry is IAuditable auditableEntity)
            {
                auditableEntity.UpdateInformation(claimsPrincipal.Identity?.Name);
            }
        }

        return await SaveChangesAsync(cancellationToken);
    }
}