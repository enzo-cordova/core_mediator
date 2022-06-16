namespace Genzai.EfCore.Tests.RepositoryTest;

/// <summary>
/// Repository tests.
/// </summary>
public class RepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryTests"/> class.
    /// </summary>
    /// <param name="fixture">Context fixture.</param>
    public RepositoryTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }

    /// <summary>
    /// Test new item.
    /// </summary>
    [Fact]
    public void Test_Add_New_Item()
    {
        var order = ObjectMock.GetOrder(1);

        var action = this.fixture.Repository.Add(order);

        action.Should().NotBeNull();
    }

    /// <summary>
    /// Test new item.
    /// </summary>
    [Fact]
    public void Test_Add_New_AuditableItem()
    {
        var order = ObjectMock.GetAuditableOrder(1);

        var action = this.fixture.RepositoryAuditable.Add(order);

        action.Should().NotBeNull();
    }

    /// <summary>
    /// Test save auditable whith claims
    /// </summary>
    [Fact]
    public async Task Test_Save_New_AuditableItem_Witch_Claims()
    {
        var order = ObjectMock.GetAuditableOrder(1);

        var context = ObjectMock.GetHttpContext();

        fixture.RepositoryAuditable.Add(order);

        var action = await fixture.RepositoryAuditable.SaveAuditableAsync(new CancellationToken(false));

        order.CreatedBy.Should().NotBeNull();
        order.CreatedBy.Should().Contain("prosegur.com");
        order.CreatedAt.Day.Should().Be(DateTime.Now.Day);
        order.IsTransient().Should().BeFalse();
        action.Should().BeTrue();
    }

    /// <summary>
    /// Test save auditable whith claims
    /// </summary>
    [Fact]
    public async Task Test_Update_New_AuditableItem_With_Claims()
    {
        var order = fixture.RepositoryAuditable.GetAll().Last();
        order.Description = "Updated Order";

        var context = ObjectMock.GetHttpContext();

        fixture.RepositoryAuditable.Update(order);

        var action = await fixture.RepositoryAuditable.SaveAuditableAsync(new CancellationToken(false));

        order.UpdatedAt.Should().NotBeNull();
        order.UpdatedBy.Should().Contain("prosegur.com");
        order.CreatedAt.Day.Should().Be(DateTime.Now.Day);
        order.IsTransient().Should().BeFalse();
        action.Should().BeTrue();
    }

    /// <summary>
    /// Test new item async.
    /// </summary>
    [Fact]
    public async Task Test_Add_New_Item_Async()
    {
        var order = ObjectMock.GetOrder(1);

        var action = await this.fixture.Repository.AddAsync(order);

        action.Should().NotBeNull();
    }

    /// <summary>
    /// Test delete item.
    /// </summary>
    [Fact]
    public void Test_Delete_Item()
    {
        var elementToDelete = this.fixture.PrepareOrders().FirstOrDefault();

        var action = this.fixture.RepositoryOrder.Delete(elementToDelete);

        action.Should().NotBeNull();
        action.State.Should().Equals(EntityState.Deleted);
    }

    /// <summary>
    /// Test get all items.
    /// </summary>
    [Fact]
    public void Test_Get_All_Items()
    {
        var totalElements = this.fixture.Context.Orders.Count();

        var action = this.fixture.Repository.GetAll();

        action.Should().NotBeEmpty();
        action.Count().Should().BeGreaterThan(0);
    }

    /// <summary>
    /// Test get all items async.
    /// </summary>
    [Fact]
    public async Task Test_Get_All_Items_Async()
    {
        var totalElements = await this.fixture.Context.Orders.CountAsync();

        var action = await this.fixture.Repository.GetAllAsync();

        action.Should().NotBeEmpty().And.HaveCount(totalElements);
    }

    /// <summary>
    /// Test get element by id.
    /// </summary>
    [Fact]
    public void Test_Get_by_Id()
    {
        var elementToQuery = this.fixture.Context.Orders.FirstOrDefault();

        var action = this.fixture.Repository.GetById(elementToQuery.Id);

        action.Should().NotBeNull().And.Equals(elementToQuery);
    }

    /// <summary>
    /// Test get element by id async.
    /// </summary>
    [Fact]
    public async Task Test_Get_by_Id_Async()
    {
        var elementToQuery = await this.fixture.Context.Orders.FirstOrDefaultAsync();

        var action = await this.fixture.Repository.GetByIdAsync(elementToQuery.Id);

        action.Should().NotBeNull().And.Equals(elementToQuery);
    }

    /// <summary>
    /// Test get filtered items.
    /// </summary>
    [Fact]
    public void Test_Get_Filtered_Items()
    {
        var element = this.fixture.Context.Orders.FirstOrDefault();

        var action = this.fixture.Repository.GetFiltered(
            predicate => predicate.Description == element.Description);

        action.Should().NotBeNull().And.Equals(element);
    }

    /// <summary>
    /// Test get filtered items async.
    /// </summary>
    [Fact]
    public async Task Test_Get_Filtered_Items_Async()
    {
        var element = await this.fixture.Context.Orders.FirstOrDefaultAsync();

        var action = await this.fixture.Repository.GetFilteredAsync(
            predicate => predicate.Description == element.Description);

        action.Should().NotBeNull().And.Equals(element);
    }

    /// <summary>
    /// Test check order by.
    /// </summary>
    [Fact]
    public void Test_Check_Order_By()
    {
        var order = new List<OrderBy>()
        {
            new OrderBy { Direction = 1, FieldName = nameof(Order.Description) }
        };

        var orderBy = new Faker<OrderByAdapter<Order>>()
            .CustomInstantiator(_ => new OrderByAdapter<Order>(order));

        var action = this.fixture.RepositoryOrder.GetAll(orderBy: orderBy.Generate().InnerExpression);

        action.Should().NotBeEmpty().And.BeInAscendingOrder(x => x.Description);
    }

    /// <summary>
    /// Test get paged items.
    /// </summary>
    [Theory]
    [InlineData(1, 5)]
    public void Test_Get_Paged_Items(int pageIndex, int pageSize)
    {
        var action = this.fixture.Repository.GetPaged(pageIndex, pageSize, _ => true);

        action.Should().NotBeNull();
        action.Elements.Should().NotBeEmpty();
    }

    /// <summary>
    /// Test get paged items async.
    /// </summary>
    [Theory]
    [InlineData(1, 5)]
    public async Task Test_Get_Paged_Items_Async(int pageIndex, int pageSize)
    {
        var action = await this.fixture.Repository.GetPagedAsync(pageIndex, pageSize, _ => true);

        action.Should().NotBeNull();
        action.Elements.Should().NotBeEmpty();
    }

    /// <summary>
    /// Test update item.
    /// </summary>
    [Fact]
    public void Test_Update_Item()
    {
        var elementToQuery = this.fixture.Context.Orders.FirstOrDefault();

        elementToQuery.Description = "test description";

        var action = this.fixture.Repository.Update(elementToQuery);

        action.Should().NotBeNull();
        action.State.Should().Equals(EntityState.Modified);
    }

    /// <summary>
    /// Test get by id with order item.
    /// </summary>
    [Fact]
    public void Test_Get_By_Id_With_OrderItem()
    {
        var element = this.fixture.PrepareOrders().FirstOrDefault();

        var include = new Faker<IncludesAdapter<Order>>()
            .CustomInstantiator(_ => new IncludesAdapter<Order>(query => query.Include(entity => entity.Buyer)));

        var action = this.fixture.RepositoryOrder.GetFiltered(
            predicate => predicate.Description == element.Description,
            include.Generate().Expression);

        action.Should().NotBeEmpty();
        action.FirstOrDefault()?.Buyer.Should().NotBeNull();
    }

    /// <summary>
    /// Test new order check domain events.
    /// </summary>
    [Fact]
    public void Test_New_Order_Check_Domain_Events()
    {
        var order = new Order(2, "Acme", new Address("Michigan ST", "Chicago", "USA"), 1, "Acme");

        order.DomainEvents.Should().NotBeEmpty();
        order.DomainEvents.FirstOrDefault()?.Should().BeOfType(typeof(OrderStartedDomainEvent));
    }

    /// <summary>
    /// Test add order events dispatched.
    /// </summary>
    [Fact]
    public async Task Test_Add_Order_Events_Dispatched()
    {
        var order = new Order(2, "Acme", new Address("Michigan ST", "Chicago", "USA"), 1, "Acme");

        await this.fixture.Repository.AddAsync(order);

        var action = await this.fixture.Context.SaveEntitiesAsync();

        action.Should().BeTrue();
    }
}