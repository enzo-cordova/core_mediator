namespace Genzai.Core.Tests.InflectorTest;

/// <summary>
/// Inflector Test.
/// </summary>
public class InflectorClassTest
{
    /// <summary>
    /// Camelize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("car_color", "carColor")]
    [InlineData("car_model", "carModel")]
    [InlineData("car_brand", "carBrand")]
    [InlineData("car_prize", "carPrize")]
    public void Test_Camelize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Camelize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Capitalize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("", "")]
    [InlineData("carColor", "CarColor")]
    [InlineData("carModel", "CarModel")]
    [InlineData("carBrand", "CarBrand")]
    [InlineData("carPrize", "CarPrize")]
    public void Test_Capitalize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Capitalize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Dasherize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("", "")]
    [InlineData("car_color", "car-color")]
    [InlineData("car_model", "car-model")]
    [InlineData("car_brand", "car-brand")]
    [InlineData("car_prize", "car-prize")]
    public void Test_Dasherize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Dasherize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Dasherize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("car_color", "Car color")]
    [InlineData("car_model", "Car model")]
    [InlineData("car_brand", "Car brand")]
    [InlineData("car_prize", "Car prize")]
    public void Test_Humanize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Humanize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Ordinalize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("1", "1st")]
    [InlineData("2", "2nd")]
    [InlineData("13", "13th")]
    [InlineData("25", "25th")]
    public void Test_Ordinalize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Ordinalize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Pascalize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("car_color", "CarColor")]
    [InlineData("car_model", "CarModel")]
    [InlineData("car_brand", "CarBrand")]
    [InlineData("car_prize", "CarPrize")]
    public void Test_Pascalize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Pascalize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Pluralize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("", "")]
    [InlineData("Car", "Cars")]
    [InlineData("Child", "Children")]
    [InlineData("Number", "Numbers")]
    [InlineData("Alias", "Aliases")]
    public void Test_Pluralize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Pluralize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Singularize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("", "")]
    [InlineData("Cars", "Car")]
    [InlineData("Children", "Child")]
    [InlineData("Numbers", "Number")]
    [InlineData("Aliases", "Alias")]
    public void Test_Singularize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Singularize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Titleize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("", "")]
    [InlineData("Cars-and-motorcyles", "Cars And Motorcyles")]
    [InlineData("sound of music", "Sound Of Music")]
    [InlineData("raining cats and dogs", "Raining Cats And Dogs")]
    [InlineData("When the dog bites and the bee stings", "When The Dog Bites And The Bee Stings")]
    public void Test_Titleize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Titleize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// UnCapitalize text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("", "")]
    [InlineData("CarColor", "carColor")]
    [InlineData("CarModel", "carModel")]
    [InlineData("CarBrand", "carBrand")]
    [InlineData("CarPrize", "carPrize")]
    public void Test_UnCapitalize(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Uncapitalize(value);

        returnedValue.Should().Be(expected);
    }

    /// <summary>
    /// Underscore text.
    /// </summary>
    /// <param name="value">value to convert.</param>
    /// <param name="expected">expected value.</param>
    [Theory]
    [InlineData("", "")]
    [InlineData("CarColor", "car_color")]
    [InlineData("CarModel", "car_model")]
    [InlineData("CarBrand", "car_brand")]
    [InlineData("CarPrize", "car_prize")]
    public void Test_Underscore(string value, string expected)
    {
        var returnedValue = Inflector.Inflector.Underscore(value);

        returnedValue.Should().Be(expected);
    }
}