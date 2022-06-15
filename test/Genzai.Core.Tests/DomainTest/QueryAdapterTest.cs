using OrderBy = Genzai.Core.Domain.QueryAdapters.OrderBy;

namespace Genzai.Core.Tests.DomainTest;

/// <summary>
/// Query Adapter tests.
/// </summary>
public class QueryAdapterTest
{
    /// <summary>
    /// Test filter query.
    /// </summary>
    [Fact]
    public void Test_Filter_Query()
    {
        //Arrange
        var carQuery = ObjectsMocks.GetCarListMock().AsQueryable();

        //Act
        var filter = new CarTestFilter(x => x.Id == 25);
        var carList = carQuery.Where(filter.InnerExpression).ToList();

        //Assert
        carList.Should().NotBeNull();
        carList.Count.Should().BeGreaterThan(0);
    }

    /// <summary>
    /// Test include Query.
    /// </summary>
    [Fact]
    public void Test_Include_Query()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<FakeDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
        var includeAdapter = new ClientTestIncludeAdapter(x => x.Include(entity => entity.Manager));

        using var context = new FakeDbContext(options);
        if (!context.Clients.Any())
        {
            context.Clients.AddRange(ObjectsMocks.GetClientListMock());

            context.SaveChanges();
        }

        //Act
        var clientQuery = includeAdapter.Expression(context.Set<ClientTest>()).FirstOrDefault();

        //Assert
        clientQuery.Manager.Should().NotBeNull();
    }

    /// <summary>
    /// Test simple order by.
    /// </summary>
    [Fact]
    public void Test_Simple_Order_By()
    {
        //Arrange
        var carQuery = ObjectsMocks.GetCarListMock().Where(_ => true).AsQueryable();
        var order = new List<OrderBy>()
        {
            new OrderBy { Direction = 1, FieldName = nameof(CarClassTest.Brand) }
        };

        //Act
        var orderBy = new CarTestOrder(order);
        var carList = orderBy.InnerExpression(carQuery).ToList();

        //Assert
        carList.Should().NotBeNull();
    }

    /// <summary>
    /// Test multiple order by.
    /// </summary>
    [Fact]
    public void Test_Multiple_Order_By()
    {
        //Arrange
        var carQuery = ObjectsMocks.GetCarListMock().Where(_ => true).AsQueryable();
        var order = new List<OrderBy>()
        {
            new OrderBy { Direction = 1, FieldName = nameof(CarClassTest.Brand) },
            new OrderBy { Direction = -1, FieldName = nameof(CarClassTest.Model) }
        };

        //Act
        var orderBy = new CarTestOrder(order);
        var carList = orderBy.InnerExpression(carQuery).ToList();

        //Assert
        carList.Should().NotBeNull();
    }
}