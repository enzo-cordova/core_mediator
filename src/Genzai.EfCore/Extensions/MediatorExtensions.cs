namespace Genzai.EfCore.Extensions;

/// <summary>
/// Mediator Extensions.
/// </summary>
public static class MediatorExtensions
{
    /// <summary>
    /// Dispatch Domain events.
    /// </summary>
    /// <typeparam name="TContext">Database Context.</typeparam>
    /// <param name="mediator">Mediator object.</param>
    /// <param name="dbContext">Database Context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static Task DispatchDomainEventsAsync<TContext>(
        this IMediator mediator,
        TContext dbContext,
        CancellationToken cancellationToken = default)
        where TContext : DbContext
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<IDomainEvent>()
            .Where(x => x.Entity.DomainEvents?.Any() == true);

        var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

        domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents.Select(x => mediator.Publish(x, cancellationToken));

        return Task.WhenAll(tasks);
    }
}