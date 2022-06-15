using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Genzai.Core.Tests.Mock;

/// <summary>
/// Objects class for getting mocks.
/// </summary>
public static class ObjectsMocks
{
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
    /// Get Cars.
    /// </summary>
    /// <returns>List of Cars.</returns>
    public static IEnumerable<CarTest> GetCars()
    {
        return new List<CarTest>()
        {
            new() { Brand = "Opel", Model = "Corsa" },
            new() { Brand = "Opel", Model = "Kadett" },
            new() { Brand = "Opel", Model = "Astra" },
            new() { Brand = "Opel", Model = "Insignia" },
            new() { Brand = "Seat", Model = "Ibiza" },
            new() { Brand = "Seat", Model = "León" },
            new() { Brand = "Seat", Model = "Toledo" },
            new() { Brand = "Seat", Model = "Córdoba" },
            new() { Brand = "Dodge", Model = "Viper" },
            new() { Brand = "Chevrolet", Model = "Corvette" },
            new() { Brand = "Ford", Model = "Focus" },
            new() { Brand = "Ford", Model = "Mustang" },
        };
    }

    /// <summary>
    /// Get Cars.
    /// </summary>
    /// <returns>List of Cars.</returns>
    public static IEnumerable<CarBaseTest> GetCarsBase()
    {
        return new List<CarBaseTest>()
        {
            new() { Brand = "Opel", Model = "Corsa" },
            new() { Brand = "Opel", Model = "Kadett" },
            new() { Brand = "Opel", Model = "Astra" },
            new() { Brand = "Opel", Model = "Insignia" },
            new() { Brand = "Seat", Model = "Ibiza" },
            new() { Brand = "Seat", Model = "León" },
            new() { Brand = "Seat", Model = "Toledo" },
            new() { Brand = "Seat", Model = "Córdoba" },
            new() { Brand = "Dodge", Model = "Viper" },
            new() { Brand = "Chevrolet", Model = "Corvette" },
            new() { Brand = "Ford", Model = "Focus" },
            new() { Brand = "Ford", Model = "Mustang" },
        };
    }

    /// <summary>
    /// Get Cars.
    /// </summary>
    /// <returns>List of Cars.</returns>
    public static IEnumerable<CarAuditableTest> GetAuditableCars()
    {
        return new List<CarAuditableTest>()
        {
            new() { Brand = "Opel", Model = "Corsa" },
            new() { Brand = "Opel", Model = "Kadett" },
            new() { Brand = "Opel", Model = "Astra",  },
            new() { Brand = "Opel", Model = "Insignia", },
            new() { Brand = "Seat", Model = "Ibiza",  },
            new() { Brand = "Seat", Model = "León" },
            new() { Brand = "Seat", Model = "Toledo"  },
            new() { Brand = "Seat", Model = "Córdoba" },
            new() { Brand = "Dodge", Model = "Viper",  },
            new() { Brand = "Chevrolet", Model = "Corvette" },
            new() { Brand = "Ford", Model = "Focus" },
            new() { Brand = "Ford", Model = "Mustang"}
        };
    }

    /// <summary>
    /// Get list of cars.
    /// </summary>
    /// <returns>List of cars</returns>
    public static IEnumerable<CarClassTest> GetCarListMock()
    {
        var carId = 1;
        var carFaker = new Faker<CarClassTest>()
            .CustomInstantiator(_ => new CarClassTest(carId++))
            .RuleFor(x => x.Brand, f => f.Name.Random.AlphaNumeric(50))
            .RuleFor(x => x.Model, f => f.Name.Random.AlphaNumeric(50));

        return carFaker.Generate(100);
    }

    /// <summary>
    /// Get car mock.
    /// </summary>
    /// <returns>CarClassTest instance.</returns>
    public static CarClassTest GetCarMock()
    {
        var carId = 1;

        return new Faker<CarClassTest>()
            .CustomInstantiator(_ => new CarClassTest(carId++))
            .RuleFor(x => x.Brand, f => f.Name.Random.AlphaNumeric(50))
            .RuleFor(x => x.Model, f => f.Name.Random.AlphaNumeric(50));
    }

    /// <summary>
    /// Get person mock.
    /// </summary>
    /// <returns>PersonClassTest instance.</returns>
    public static PersonClassTest GetPersonMock()
    {
        return new Faker<PersonClassTest>()
            .CustomInstantiator(x => new PersonClassTest(x.Name.FirstName(Name.Gender.Male)));
    }

    /// <summary>
    /// Get Clients List.
    /// </summary>
    /// <returns>ClientTest list.</returns>
    public static IEnumerable<ClientTest> GetClientListMock()
    {
        var clientId = 1;
        var managerId = 1;

        var managerFaker = new Faker<ManagerTest>()
            .CustomInstantiator(x =>
                new ManagerTest(managerId++, x.Name.FirstName(Name.Gender.Male), x.Name.LastName()));

        var clientFaker = new Faker<ClientTest>().CustomInstantiator(x =>
            new ClientTest(clientId++, x.Name.FirstName(Name.Gender.Male), x.Name.LastName(), managerFaker.Generate()));

        return clientFaker.Generate(20);
    }

    /// <summary>
    /// Get Orders mock.
    /// </summary>
    /// <returns>List of orders.</returns>
    public static IEnumerable<Order> GetOrders(int buyerId)
    {
        int productId = 1;
        var address = new Faker<Genzai.Core.Tests.Mock.OrderAggregate.Address>()
            .CustomInstantiator(x => new Genzai.Core.Tests.Mock.OrderAggregate.Address(x.Address.StreetName(), x.Address.City(), x.Address.Country()));
        var order = new Faker<Order>()
            .CustomInstantiator(x => new Order(buyerId, x.Commerce.ProductMaterial(), address.Generate()));
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
}