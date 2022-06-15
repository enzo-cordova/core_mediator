namespace Genzai.CosmosDb.Extensions;

/// <summary>
/// Mediator Extensions.
/// </summary>
public static class MediatorExtensions
{
    /// <summary>
    /// Dispatch Domain events.
    /// </summary>
    /// <typeparam name="TEntity">Cosmos Entity.</typeparam>
    /// <param name="mediator">Mediator object.</param>
    /// <param name="entity">Cosmos Entity.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static async Task DispatchDomainEventsAsync<TEntity>(
        this IMediator mediator,
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : CosmosEntityDomain
    {
        var domainEvents = entity.DomainEvents;
        var tasks = domainEvents.Select(x => mediator.Publish(x, cancellationToken));

        await Task.WhenAll(tasks);

        entity.ClearDomainEvents();
    }
}