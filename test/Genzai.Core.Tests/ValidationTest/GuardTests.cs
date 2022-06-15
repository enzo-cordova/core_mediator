namespace Genzai.Core.Tests.ValidationTest;

/// <summary>
/// Class for test guards.
/// </summary>
public class GuardTests
{
    /// <summary>
    /// Test against Argument null exception.
    /// </summary>
    [Fact]
    public void Test_Against_Some_Object_Null()
    {
        //Arrange
        CarClassTest nullCar = null;

        //Act
        Action guardAction = () => Guard.Against<ArgumentNullException>(nullCar == null, "Car cannot be null");

        //Assert
        guardAction.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'Car cannot be null')");
    }

    /// <summary>
    /// Test against Argument null exception with messages params.
    /// </summary>
    [Fact]
    public void Test_Against_Some_Object_Null_With_Message_Params()
    {
        //Arrange
        CarClassTest nullCar = null;

        //Act
        Action guardAction = () => Guard.Against<ArgumentNullException>(nullCar == null, "{0} cannot be null", nameof(CarClassTest));

        //Assert
        guardAction.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'CarClassTest cannot be null')");
    }

    /// <summary>
    /// Test against Argument null exception with func.
    /// </summary>
    [Fact]
    public void Test_Against_Func_Result()
    {
        //Arrange
        static bool func() => true;

        //Act
        Action guardAction = () => Guard.Against<ArgumentNullException>(func, "Function cannot be null");

        //Assert
        guardAction.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'Function cannot be null')");
    }

    /// <summary>
    /// Test if an object implements an interface.
    /// </summary>
    [Fact]
    public void Test_The_Object_Implements_Interface()
    {
        //Arrange
        var person = ObjectsMocks.GetPersonMock();

        //Act
        Action guardAction = () => Guard.Implements<IDisposable>(person, "The class must implement IDisposable.");

        //Assert
        guardAction.Should().Throw<InvalidOperationException>().WithMessage("The class must implement IDisposable.");
    }

    /// <summary>
    /// Test if an object implements an interface passing Type.
    /// </summary>
    [Fact]
    public void Test_The_Object_Implements_Interface_Passing_Type()
    {
        //Arrange
        var person = ObjectsMocks.GetPersonMock();

        //Act
        Action guardAction = () => Guard.Implements<IDisposable>(person.GetType(), "The class must implement IDisposable.");

        //Assert
        guardAction.Should().Throw<InvalidOperationException>().WithMessage("The class must implement IDisposable.");
    }

    /// <summary>
    /// Test if an object inherits base class.
    /// </summary>
    [Fact]
    public void Test_The_Object_Inherits_Base_Class()
    {
        //Arrange
        var person = ObjectsMocks.GetPersonMock();

        //Act
        Action guardAction = () => Guard.InheritsFrom<Exception>(person, "The class must inherit Exception class.");

        //Assert
        guardAction.Should().Throw<InvalidOperationException>().WithMessage("The class must inherit Exception class.");
    }

    /// <summary>
    /// Test if an object inherits base class passing type.
    /// </summary>
    [Fact]
    public void Test_The_Object_Inherits_Base_Class_Passing_Type()
    {
        //Arrange
        var person = ObjectsMocks.GetPersonMock();

        //Act
        Action guardAction = () => Guard.InheritsFrom<Exception>(person.GetType(), "The class must inherit Exception class.");

        //Assert
        guardAction.Should().Throw<InvalidOperationException>().WithMessage("The class must inherit Exception class.");
    }

    /// <summary>
    /// Test if an object is equal to an other.
    /// </summary>
    [Fact]
    public void Test_Object_IsEqual_To_Another()
    {
        //Arrange
        var person = ObjectsMocks.GetPersonMock();
        var car = ObjectsMocks.GetCarMock();

        //Act
        Action guardAction = () => Guard.IsEqual<ArgumentException>(person, car, "Objects are not equal.");

        //Assert
        guardAction.Should().Throw<ArgumentException>().WithMessage("Objects are not equal.");
    }

    /// <summary>
    /// Test object is not null.
    /// </summary>
    [Fact]
    public void Test_Object_Is_Not_Null()
    {
        //Arrange
        PersonClassTest person = null;

        //Act
        Action guardAction = () => Guard.IsNotNull(person, "Person is null");

        //Assert
        guardAction.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'Person is null')");
    }

    /// <summary>
    /// Test string is null or empty.
    /// </summary>
    [Fact]
    public void Test_String_Is_Not_Null_Or_Empty()
    {
        //Arrange
        var stringTest = string.Empty;

        //Act
        Action guardAction = () => Guard.IsNotNullNorEmpty(stringTest, "String test is empty");

        //Assert
        guardAction.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'String test is empty')");
    }

    /// <summary>
    /// Test string is not white space.
    /// </summary>
    [Fact]
    public void Test_String_Is_Not_Null_Or_White_Space()
    {
        //Arrange
        const string stringTest = "";

        //Act
        Action guardAction = () => Guard.IsNotNullNorEmpty(stringTest, "String test is empty");

        //Assert
        guardAction.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'String test is empty')");
    }

    /// <summary>
    /// Test object is specified type.
    /// </summary>
    [Fact]
    public void Test_Object_Is_Specified_Type()
    {
        //Arrange
        var person = ObjectsMocks.GetPersonMock();

        //Act
        Action guardAction = () => Guard.TypeOf<CarClassTest>(person, "Person is not a Car.");

        //Assert
        guardAction.Should().Throw<InvalidOperationException>().WithMessage("Person is not a Car.");
    }
}