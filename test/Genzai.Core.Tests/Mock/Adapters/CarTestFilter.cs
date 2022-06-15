namespace Genzai.Core.Tests.Mock.Adapters;

/// <summary>
/// Car test filter.
/// </summary>
public class CarTestFilter : FilterAdapter<CarClassTest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarTestFilter"/> class.
    /// </summary>
    /// <param name="expression"></param>
    public CarTestFilter(Expression<Func<CarClassTest, bool>> expression)
        : base(expression)
    {
    }
}