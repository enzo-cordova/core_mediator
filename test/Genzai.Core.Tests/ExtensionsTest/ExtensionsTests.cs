using Microsoft.AspNetCore.Http;

namespace Genzai.Core.Tests.ExtensionsTest;

/// <summary>
/// Extensions test.
/// </summary>
public class ExtensionsTests : IClassFixture<CarListFixture>
{
    private readonly CarListFixture carList;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionsTests"/> class.
    /// </summary>
    public ExtensionsTests(CarListFixture carList)
    {
        this.carList = carList;
    }

    /// <summary>
    /// Enum test.
    /// </summary>
    [Fact]
    public void Test_Cars_Enum_Members()
    {
        var hatchBack = CarsEnumTest.HatchBack.GetEnumMemberAttributeValue();
        var crossOver = CarsEnumTest.CrossOver.GetEnumMemberAttributeValue();
        var coupe = CarsEnumTest.Coupe.GetEnumMemberAttributeValue();
        var sedan = CarsEnumTest.Sedan.GetEnumMemberAttributeValue();

        hatchBack.Should().Be("Hatchback");
        crossOver.Should().Be("CrossOver");
        coupe.Should().BeEmpty();
        sedan.Should().BeEmpty();
    }

    /// <summary>
    /// Test paging for IEnumerable.
    /// </summary>
    [Fact]
    public void Test_For_Pagging_IEnumerable()
    {
        var list = this.carList.CarList.Page(1, 10).ToList();

        list.Count.Should().BeGreaterThan(0);

        foreach (var car in list)
        {
            car.Brand.Should().NotBeEmpty();
            car.Model.Should().NotBeEmpty();
        }
    }

    /// <summary>
    /// Test paging for IQueryable.
    /// </summary>
    [Fact]
    public void Test_For_Paging_IQueryable()
    {
        var query = this.carList.CarList.Where(k => !string.IsNullOrEmpty(k.Model)).AsQueryable();
        var list = query.Page(1, 10).ToList();

        list.Count.Should().BeGreaterThan(0);

        foreach (var car in list)
        {
            car.Brand.Should().NotBeEmpty();
            car.Model.Should().NotBeEmpty();
        }
    }

    /// <summary>
    /// Test get full path of filename.
    /// </summary>
    [Fact]
    public void Test_Get_Full_Path_FileName()
    {
        //Arrange
        const string fileName = "file.js";

        //Act
        var fullPath = fileName.GetFilePathFromBasePath();

        //Assert
        fullPath.Should().NotBeNullOrEmpty();
        fullPath.Should().Contain(fileName);
        Assert.Equal(Path.Combine(ApplicationEnvironment.ApplicationBasePath, fileName), fullPath);

        // fullPath.Should().Equals(Path.Combine(ApplicationEnvironment.ApplicationBasePath, fileName));
    }

    /// <summary>
    /// Test Validator.
    /// </summary>
    [Fact]
    public void Test_Validator()
    {
        var car = ObjectsMocks.GetCars().FirstOrDefault();
        var validator = new CarTestValidator();

        Func<Task> action = async () => await validator.ValidateCommand(car);

        action.Should().NotThrowAsync();
    }

    /// <summary>
    /// Test Validator.
    /// </summary>
    [Fact]
    public void Test_Validator_Not_Passed()
    {
        var car = new CarTest();
        var validator = new CarTestValidator();

        Func<Task> action = async () => await validator.ValidateCommand(car);

        action.Should().ThrowAsync<ArgumentException>();
    }

    /// <summary>
    /// Test Security Extension.
    /// </summary>
    [Fact]
    public void Test_Security_Extension()
    {
        var servicesMock = new Mock<IServiceCollection>();
        var inMemorySettings = new Dictionary<string, string> {
            {"JWT:Key", "u43u3uidh"},
            {"JWT:Audience", "test"},
            {"JWT:Issuer", "issuertest"},
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        Action act = () => servicesMock.Object.AddGenzaiAuthenticationBearer(configuration);

        act.Should().NotThrow<NullReferenceException>();
    }

    /// <summary>
    /// Test Exception Context ArgumentExceptionHandler
    /// </summary>
    [Fact]
    public void Test_Exception_Context_ArgumentExceptionHandler()
    {
        var filterMetadata = new Mock<IList<IFilterMetadata>>();
        var routes = new Mock<IReadOnlyDictionary<string, object>>();
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var mockException = new Mock<Exception>();

        mockException.Setup(e => e.StackTrace)
            .Returns("Test stacktrace");
        mockException.Setup(e => e.Message)
            .Returns("Test message");
        mockException.Setup(e => e.Source)
            .Returns("Test source");

        var exceptionContext = new ExceptionContext(actionContext, filterMetadata.Object)
        {
            Exception = mockException.Object
        };

        Action act = () => exceptionContext.ArgumentExceptionHandler();

        act.Should().NotThrow();
    }

    /// <summary>
    /// Test Exception Context BadRequestHandler
    /// </summary>
    [Fact]
    public void Test_Exception_Context_BadRequestHandler()
    {
        var environmentMock = new Mock<IWebHostEnvironment>();
        var filterMetadata = new Mock<IList<IFilterMetadata>>();
        var routes = new Mock<IReadOnlyDictionary<string, object>>();
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var mockException = new Mock<Exception>();

        mockException.Setup(e => e.StackTrace)
            .Returns("Test stacktrace");
        mockException.Setup(e => e.Message)
            .Returns("Test message");
        mockException.Setup(e => e.Source)
            .Returns("Test source");

        var exceptionContext = new ExceptionContext(actionContext, filterMetadata.Object)
        {
            Exception = mockException.Object
        };

        Action act = () => exceptionContext.BadRequestHandler(environmentMock.Object, "Develop Message");

        act.Should().NotThrow();
    }

    /// <summary>
    /// Test Exception Context ServerErrorHandler
    /// </summary>
    [Fact]
    public void Test_Exception_Context_ServerErrorHandler()
    {
        var environmentMock = new Mock<IWebHostEnvironment>();
        var filterMetadata = new Mock<IList<IFilterMetadata>>();
        var routes = new Mock<IReadOnlyDictionary<string, object>>();
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var mockException = new Mock<Exception>();

        mockException.Setup(e => e.StackTrace)
            .Returns("Test stacktrace");
        mockException.Setup(e => e.Message)
            .Returns("Test message");
        mockException.Setup(e => e.Source)
            .Returns("Test source");

        var exceptionContext = new ExceptionContext(actionContext, filterMetadata.Object)
        {
            Exception = mockException.Object
        };

        Action act = () => exceptionContext.ServerErrorHandler(environmentMock.Object);

        act.Should().NotThrow();
    }
}