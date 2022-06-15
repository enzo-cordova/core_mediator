namespace Genzai.CosmosDb.Tests.Mock.Repository;

/// <summary>
/// Client Repository.
/// </summary>
public class ClientRepository : Repository<Client>, IClientRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientRepository"/> class.
    /// </summary>
    /// <param name="mediator">Mediator service.</param>
    /// <param name="container">Cosmos container.</param>
    public ClientRepository(IMediator mediator, IMongoCollection<Client> collection)
        : base(mediator, collection)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientRepository"/> class.
    /// </summary>
    /// <param name="mediator">Mediator service.</param>
    /// <param name="context">Cosmos context.</param>
    /// <param name="collectionName"></param>
    public ClientRepository(IMediator mediator, ICosmosDbContext context, string collectionName)
        : base(mediator, context, collectionName)
    {
    }
}