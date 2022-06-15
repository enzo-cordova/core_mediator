using OrderBy = Genzai.Core.Domain.QueryAdapters.OrderBy;

namespace Genzai.Core.Tests.Mock.Adapters;

/// <summary>
/// Car test order by adapter.
/// </summary>
public class CarTestOrder : OrderByAdapter<CarClassTest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarTestOrder"/> class.
    /// </summary>
    /// <param name="orderByList">Order list.</param>
    public CarTestOrder(List<OrderBy> orderByList)
        : base(orderByList)
    {
    }
}