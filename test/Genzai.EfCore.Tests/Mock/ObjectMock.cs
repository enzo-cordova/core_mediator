using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Genzai.EfCore.Tests.Mock;

/// <summary>
/// Object Mocking.
/// </summary>
public static class ObjectMock
{
    private const int userId = 1;
    private const string userName = "UserName";

    /// <summary>
    /// Mocked HttpContext
    /// </summary>
    /// <returns></returns>
    public static ControllerContext GetHttpContext()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new("emails", "mock@email.fake"),
            new(ClaimTypes.Name, "anynameazuread@prosegur.com")
        }, "mock"));

        return new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    /// <summary>
    /// Get buyers fake.
    /// </summary>
    /// <returns>Buyers list</returns>
    public static IEnumerable<Buyer> GetBuyers()
    {
        var buyer = new Faker<Buyer>()
            .CustomInstantiator(x => new Buyer(x.Company.CompanyName()));

        return buyer.Generate(5);
    }

    /// <summary>
    /// Get Orders mock.
    /// </summary>
    /// <returns>List of orders.</returns>
    public static IEnumerable<Order> GetOrders(int buyerId)
    {
        int productId = 1;
        var address = new Faker<Address>()
            .CustomInstantiator(x => new Address(x.Address.StreetName(), x.Address.City(), x.Address.Country()));
        var order = new Faker<Order>()
            .CustomInstantiator(x => new Order(buyerId, x.Commerce.ProductMaterial(), address.Generate(), userId, userName));
        var orderItem = new Faker<OrderItem>()
            .CustomInstantiator(x => new OrderItem(
                productId++, x.Commerce.ProductName(), x.Random.Decimal(min: 1, max: 200), x.Random.Int(min: 1, max: 10)));

        var orderList = order.Generate(10);

        orderList.ForEach(k =>
        {
            var item = orderItem.Generate();

            k.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Units);
        });

        return orderList;
    }

    /// <summary>
    /// Get mock order.
    /// </summary>
    /// <returns>Order mock.</returns>
    public static Order GetOrder(int buyerId)
    {
        var address = new Faker<Address>()
            .CustomInstantiator(x => new Address(x.Address.StreetName(), x.Address.City(), x.Address.Country()));
        var order = new Faker<Order>()
            .CustomInstantiator(x => new Order(buyerId, x.Commerce.ProductMaterial(), address.Generate(), userId, userName));

        return order.Generate();
    }

    /// <summary>
    /// Get mock order.
    /// </summary>
    /// <returns>Order mock.</returns>
    public static AuditableOrder GetAuditableOrder(int buyerId)
    {
        var address = new Faker<Address>()
            .CustomInstantiator(x => new Address(x.Address.StreetName(), x.Address.City(), x.Address.Country()));
        var order = new Faker<AuditableOrder>()
            .CustomInstantiator(x => new AuditableOrder(buyerId, x.Commerce.ProductMaterial(), address.Generate(), userId, userName));

        return order.Generate();
    }

    /// <summary>
    /// Get order item.
    /// </summary>
    /// <returns>OrderItem mock.</returns>
    public static OrderItem GetOrderItem()
    {
        int productId = 1;

        var orderItem = new Faker<OrderItem>()
            .CustomInstantiator(x => new OrderItem(
                productId++, x.Commerce.ProductName(), x.Random.Decimal(min: 1, max: 200), x.Random.Int(min: 1, max: 10)));

        return orderItem.Generate();
    }
}