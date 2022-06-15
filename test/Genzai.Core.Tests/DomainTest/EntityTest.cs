namespace Genzai.Core.Tests.DomainTest;

/// <summary>
/// Entity test class for testing domain base class
/// </summary>
public class EntityTest : IDisposable
{
    private bool disposedValue = false;

    private readonly EntityFakeDbContext contextFixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityTest"/> class.
    /// </summary>
    public EntityTest()
    {
        var options = new DbContextOptionsBuilder<EntityFakeDbContext>()
            .UseInMemoryDatabase(databaseName: "EntityTest")
            .Options;

        this.contextFixture = new EntityFakeDbContext(options);

        contextFixture.Database.EnsureDeleted();
        contextFixture.Database.EnsureCreated();

        if (!this.contextFixture.Cars.Any())
        {
            this.contextFixture.Cars.AddRange(ObjectsMocks.GetCars());

            this.contextFixture.SaveChanges();
        }

        if (!this.contextFixture.CarsAuditables.Any())
        {
            contextFixture.CarsAuditables.AddRange(ObjectsMocks.GetAuditableCars());
            var context = ObjectsMocks.GetHttpContext();
            contextFixture.SaveAuditChanges(context.HttpContext.User, new CancellationToken(false));
        }

        if (!contextFixture.CarsBase.Any())
        {
            contextFixture.CarsBase.AddRange(ObjectsMocks.GetCarsBase());
            contextFixture.SaveChanges();
        }
    }

    /// <summary>
    /// Dispose Method.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }

    /// <summary>
    /// Create Entity Test.
    /// </summary>
    [Fact]
    public void Test_Create_Entity()
    {
        var entity = new CarTest { Brand = "Peugeot", Model = "206" };
        var hashCode = entity.GetHashCode();
        var transient = entity.IsTransient();

        entity.Should().NotBeNull();
        entity.Model.Should().Be("206");
        entity.Brand.Should().Be("Peugeot");
        hashCode.Should().BeInRange(int.MinValue, int.MaxValue);
        transient.Should().BeTrue();
    }

    /// <summary>
    /// Create Entity Test.
    /// </summary>
    [Fact]
    public void Test_Create_AuditableEntity_With_Context()
    {
        var entity = new CarAuditableTest { Brand = "Peugeot", Model = "206" };

        var context = ObjectsMocks.GetHttpContext();

        contextFixture.CarsAuditables.Add(entity);

        contextFixture.SaveAuditChanges(context.HttpContext.User, new CancellationToken(false));

        var hashCode = entity.GetHashCode();
        var transient = entity.IsTransient();

        entity.Should().NotBeNull();
        entity.Model.Should().Be("206");
        entity.Brand.Should().Be("Peugeot");
        entity.CreatedAt.Date.Day.Should().Be(DateTime.Now.Day);
        entity.CreatedBy.Should().NotBeEmpty();
        hashCode.Should().BeInRange(int.MinValue, int.MaxValue);
        transient.Should().BeFalse();
    }

    /// <summary>
    /// Create Entity Test.
    /// </summary>
    [Fact]
    public void Test_Create_BaseEntity()
    {
        var entity = new CarBaseTest { Brand = "Peugeot", Model = "206" };

        contextFixture.CarsBase.Add(entity);

        contextFixture.SaveChanges();

        var hashCode = entity.GetHashCode();

        entity.Should().NotBeNull();
        entity.Model.Should().Be("206");
        entity.Brand.Should().Be("Peugeot");
        hashCode.Should().BeInRange(int.MinValue, int.MaxValue);
    }

    /// <summary>
    /// Create Entity Test.
    /// </summary>
    [Fact]
    public void Test_Update_AuditableEntity_With_User()
    {
        var context = ObjectsMocks.GetHttpContext();
        var entity = contextFixture.CarsAuditables.FirstOrDefault(c => c.Brand == "Opel");

        contextFixture.Update<CarAuditableTest>(entity);
        contextFixture.SaveAuditChanges(context.HttpContext.User, new CancellationToken(false));

        entity.Should().NotBeNull();
        entity.Brand.Should().Be("Opel");
        entity.CreatedAt.Should().HaveDay(DateTime.Now.Day);
        entity.UpdatedAt.Should().HaveDay(DateTime.Now.Day);
    }

    /// <summary>
    /// Create Entity Test.
    /// </summary>
    [Fact]
    public void Test_Update_BaseEntity()
    {
        var entity = contextFixture.CarsBase.FirstOrDefault(c => c.Brand == "Opel");
        contextFixture.CarsBase.Remove(entity);
        contextFixture.SaveChanges();

        entity.Brand = "blabla";

        contextFixture.CarsBase.Add(entity);
        contextFixture.SaveChanges();

        entity.Should().NotBeNull();
        entity.Brand.Should().Be("blabla");
    }

    /// <summary>
    /// Test No Transient entity.
    /// </summary>
    [Fact]
    public void Test_No_Transient_Entity()
    {
        //Arrange
        var carFromQuery = this.contextFixture.Cars.FirstOrDefault();

        //Act
        var hashCode = carFromQuery.GetHashCode();
        var transient = carFromQuery.IsTransient();

        //Assert
        carFromQuery.Should().NotBeNull();
        hashCode.Should().BeInRange(int.MinValue, int.MaxValue);
        transient.Should().BeFalse();
    }

    /// <summary>
    /// Test No Transient entity.
    /// </summary>
    [Fact]
    public void Test_No_Transient_BaseEntity()
    {
        //Arrange
        var carFromQuery = this.contextFixture.CarsBase.FirstOrDefault();

        //Act
        var hashCode = carFromQuery.GetHashCode();

        //Assert
        carFromQuery.Should().NotBeNull();
        hashCode.Should().BeInRange(int.MinValue, int.MaxValue);
    }

    /// <summary>
    /// Compare Entities Test.
    /// </summary>
    /// <param name="brand">brand text.</param>
    /// <param name="model">model text.</param>
    [Theory]
    [InlineData("Peugeot", "206")]
    [InlineData("Seat", "Ibiza")]
    [InlineData("BMW", "320")]
    public void Test_Compare_Entities(string brand, string model)
    {
        var entity = new CarTest { Brand = brand, Model = model };
        var list = ObjectsMocks.GetCars().ToList();
        var element = list.Find(k => k.Model == model && k.Brand == brand);

        if (element != null)
        {
            entity.Should().NotBe(element);
            entity.Model.Should().Be(element.Model);
            entity.Brand.Should().Be(element.Brand);
        }
        else
        {
            list.Add(entity);

            element.Should().BeNull();
            element = list.Find(k => k.Model == model && k.Brand == brand);

            if (entity == element)
            {
                entity.Should().Be(element);
                entity.Model.Should().Be(element.Model);
                entity.Brand.Should().Be(element.Brand);
            }
        }
    }

    /// <summary>
    /// Compare Entities Test.
    /// </summary>
    /// <param name="brand">brand text.</param>
    /// <param name="model">model text.</param>
    [Theory]
    [InlineData("Peugeot", "206")]
    [InlineData("Seat", "Ibiza")]
    [InlineData("BMW", "320")]
    public void Test_Compare_AuditableEntities(string brand, string model)
    {
        var entity = new CarAuditableTest { Brand = brand, Model = model };
        var list = ObjectsMocks.GetAuditableCars().ToList();
        var element = list.Find(k => k.Model == model && k.Brand == brand);

        if (element != null)
        {
            entity.Should().NotBe(element);
            entity.Model.Should().Be(element.Model);
            entity.Brand.Should().Be(element.Brand);
        }
        else
        {
            list.Add(entity);

            element.Should().BeNull();
            element = list.Find(k => k.Model == model && k.Brand == brand);

            if (entity == element)
            {
                entity.Should().Be(element);
                entity.Model.Should().Be(element.Model);
                entity.Brand.Should().Be(element.Brand);
            }
        }
    }

    /// <summary>
    /// Test Base entity
    /// </summary>
    /// <param name="brand"></param>
    /// <param name="model"></param>
    [Theory]
    [InlineData("Peugeot", "206")]
    [InlineData("Seat", "Ibiza")]
    [InlineData("BMW", "320")]
    public void Test_Compare_BaseEntities(string brand, string model)
    {
        var entity = new CarBaseTest() { Brand = brand, Model = model };
        var list = ObjectsMocks.GetCarsBase().ToList();
        var element = list.Find(k => k.Model == model && k.Brand == brand);

        if (element != null)
        {
            entity.Should().NotBe(element);
            entity.Model.Should().Be(element.Model);
            entity.Brand.Should().Be(element.Brand);
        }
        else
        {
            list.Add(entity);

            element.Should().BeNull();
            element = list.Find(k => k.Model == model && k.Brand == brand);

            if (entity == element)
            {
                entity.Should().Be(element);
                entity.Model.Should().Be(element.Model);
                entity.Brand.Should().Be(element.Brand);
            }
        }
    }

    /// <summary>
    /// Test Entity Equals
    /// </summary>
    [Fact]
    public void Test_Entity_Equals()
    {
        var list = ObjectsMocks.GetCars().ToList();

        var act = list[0].Equals(list[1]);

        act.Should().BeFalse();
    }

    /// <summary>
    /// Test Entity Equals
    /// </summary>
    [Fact]
    public void Test_AuditableEntity_Equals()
    {
        var list = ObjectsMocks.GetAuditableCars().ToList();

        var act = list[0].Equals(list[1]);

        act.Should().BeFalse();
    }

    /// <summary>
    /// Test Entity Equals
    /// </summary>
    [Fact]
    public void Test_BaseEntity_Equals_Not_True()
    {
        var list = ObjectsMocks.GetCarsBase().ToList();

        var act = list[0].Equals(list[1]);

        act.Should().BeFalse();
    }

    /// <summary>
    /// Test Entity Equials
    /// </summary>
    [Fact]
    public void Test_BaseEntity_Equals_True()
    {
        var list = ObjectsMocks.GetCarsBase().ToList();

        var elementA = list[0];
        var elementB = (object)list[0];

        var act = elementA.Equals(elementB);

        act.Should().BeTrue();
    }

    /// <summary>
    /// Test Entity Equials
    /// </summary>
    [Fact]
    public void Test_BaseEntity_Equals_True_Object()
    {
        var list = ObjectsMocks.GetCarsBase().ToList();

        var elementA = list[0];
        var elementB = new Object() { };

        var act = elementA.Equals(elementB);

        act.Should().BeFalse();
    }

    /// <summary>
    /// Paged Elements Test.
    /// </summary>
    [Fact]
    public void Test_Paged_Elements()
    {
        var elements = this.contextFixture.Cars;
        var total = elements.ToList().Count;
        var pagedElements = new PagedElements<CarTest>(elements, total);
        var totalPages = pagedElements.TotalPages(5);

        pagedElements.Should().NotBeNull();

        //pagedElements.TotalElements.Should().Equals(total);

        totalPages.Should().BeGreaterThan(0);
        pagedElements.Elements.Should().NotBeEmpty();

        foreach (var car in pagedElements.Elements)
        {
            car.Should().NotBeNull();
            car.Model.Should().NotBeEmpty();
            car.Brand.Should().NotBeEmpty();
            car.GetHashCode().Should().BeInRange(int.MinValue, int.MaxValue);
        }
    }

    /// <summary>
    /// Paged Elements Test.
    /// </summary>
    [Fact]
    public void Test_AuditablePaged_Elements()
    {
        var elements = this.contextFixture.CarsAuditables;
        var total = elements.ToList().Count;
        var pagedElements = new PagedElements<CarAuditableTest>(elements, total);
        var totalPages = pagedElements.TotalPages(5);

        pagedElements.Should().NotBeNull();

        //pagedElements.TotalElements.Should().Equals(total);

        totalPages.Should().BeGreaterThan(0);
        pagedElements.Elements.Should().NotBeEmpty();

        foreach (var car in pagedElements.Elements)
        {
            car.Should().NotBeNull();
            car.Model.Should().NotBeEmpty();
            car.Brand.Should().NotBeEmpty();
            car.GetHashCode().Should().BeInRange(int.MinValue, int.MaxValue);
        }
    }

    /// <summary>
    /// Paged Elements Test.
    /// </summary>
    [Fact]
    public void Test_BasePaged_Elements()
    {
        var elements = this.contextFixture.CarsBase;
        var total = elements.ToList().Count;
        var pagedElements = new PagedElements<CarBaseTest>(elements, total);
        var totalPages = pagedElements.TotalPages(5);

        pagedElements.Should().NotBeNull();

        //pagedElements.TotalElements.Should().Equals(total);

        totalPages.Should().BeGreaterThan(0);
        pagedElements.Elements.Should().NotBeEmpty();

        foreach (var car in pagedElements.Elements)
        {
            car.Should().NotBeNull();
            car.Model.Should().NotBeEmpty();
            car.Brand.Should().NotBeEmpty();
            car.GetHashCode().Should().BeInRange(int.MinValue, int.MaxValue);
        }
    }

    /// <summary>
    /// Dispose method.
    /// </summary>
    /// <param name="disposing">Disposing param.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            this.contextFixture.Dispose();

            disposedValue = true;
        }
    }
}