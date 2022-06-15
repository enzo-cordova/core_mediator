namespace Genzai.CosmosDb.Tests.Mock.Entities;

/// <summary>
/// Client order by.
/// </summary>
public class ClientOrder : OrderByAdapter<Client>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientOrder"/> class.
    /// </summary>
    /// <param name="orderByList">Order list.</param>
    public ClientOrder(List<OrderBy> orderByList)
        : base(orderByList)
    {
    }
}