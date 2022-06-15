namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Person classs test for guard testing.
/// </summary>
public class PersonClassTest : BaseClass, IBaseInterface
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseClass"/> class.
    /// </summary>
    /// <param name="parentName">parent name string.</param>
    public PersonClassTest(string parentName) : base(parentName)
    {
    }

    /// <summary>
    /// Get true value.
    /// </summary>
    /// <seealso cref="IBaseInterface.GetInterfaceProperty" />
    public bool GetInterfaceProperty()
    {
        return true;
    }
}