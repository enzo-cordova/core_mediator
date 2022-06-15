namespace Genzai.EfCore.Tests.XunitHelper;

/// <summary>
/// Attribute for set execution test priority.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TestPriorityAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TestPriorityAttribute"/> class.
    /// </summary>
    /// <param name="priority">Set priority.</param>
    public TestPriorityAttribute(int priority)
    {
        Priority = priority;
    }

    /// <summary>
    /// Gets priority.
    /// </summary>
    public int Priority { get; }
}