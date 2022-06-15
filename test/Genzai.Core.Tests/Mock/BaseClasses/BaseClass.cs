namespace Genzai.Core.Tests.Mock.BaseClasses;

/// <summary>
/// Base class for testing guards.
/// </summary>
public abstract class BaseClass
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseClass"/> class.
    /// </summary>
    /// <param name="parentName">parent name string.</param>
    protected BaseClass(string parentName)
    {
        this.ParentName = parentName;
    }

    /// <summary>
    /// Gets or sets Parent name member.
    /// </summary>
    public string ParentName { get; set; }
}