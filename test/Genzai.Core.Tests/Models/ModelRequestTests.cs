namespace Genzai.Core.Tests.Models;

/// <summary>
/// Model request test.
/// </summary>
public class ModelRequestTests
{
    /// <summary>
    /// Test request.
    /// </summary>
    [Fact]
    public void Test_Request()
    {
        var manager = new Faker<ManagerTest>()
            .CustomInstantiator(x => new ManagerTest(1, x.Name.FirstName(Name.Gender.Male), x.Name.LastName()));
        var arrange = new Faker<ManagerRequest>()
            .RuleFor(x => x.Model, () => manager.Generate(1).FirstOrDefault());

        var action = arrange.Generate(1).FirstOrDefault();

        action.Should().NotBeNull();
        action.Model.Should().NotBeNull();
    }

    /// <summary>
    /// Test paged request.
    /// </summary>
    [Fact]
    public void Test_Paged_Request()
    {
        var manager = new Faker<ManagerTest>()
            .CustomInstantiator(x => new ManagerTest(1, x.Name.FirstName(Name.Gender.Male), x.Name.LastName()));
        var arrange = new Faker<ManagerPagedRequest>()
            .RuleFor(x => x.Model, () => manager.Generate(1).FirstOrDefault())
            .RuleFor(x => x.Page, 0)
            .RuleFor(x => x.PageSize, 10);

        var action = arrange.Generate(1).FirstOrDefault();

        action.Should().NotBeNull();
        action.Model.Should().NotBeNull();
        action.PageSize.Should().Be(10);
        action.Page.Should().Be(0);
    }

    /// <summary>
    /// Tests ordered request.
    /// </summary>
    [Fact]
    public void Test_Ordered_Request()
    {
        var order = new Genzai.Core.Domain.QueryAdapters.OrderBy
        {
            Direction = 0,
            FieldName = nameof(ManagerTest.LastName)
        };
        var manager = new Faker<ManagerTest>()
            .CustomInstantiator(x => new ManagerTest(1, x.Name.FirstName(Name.Gender.Male), x.Name.LastName()));
        var arrange = new Faker<ManagerOrderRequest>()
            .RuleFor(x => x.Model, () => manager.Generate(1).FirstOrDefault())
            .RuleFor(x => x.Page, () => 0)
            .RuleFor(x => x.PageSize, () => 10)
            .RuleFor(x => x.OrderingList, () => new List<OrderBy> { order });

        var action = arrange.Generate(1).FirstOrDefault();
        var actionOrder = action.OrderingList.FirstOrDefault();

        action.Should().NotBeNull();
        action?.Model.Should().NotBeNull();
        action?.PageSize.Should().Be(10);
        action?.Page.Should().Be(0);
        action?.OrderingList.Should().NotBeEmpty();
        //actionOrder.Direction.Should().NotBeNull();
        actionOrder?.FieldName.Should().NotBeNullOrEmpty();
    }
}