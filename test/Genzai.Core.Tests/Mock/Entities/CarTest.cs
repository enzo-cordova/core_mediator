namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Car Test Entity.
/// </summary>
public class CarTest : Entity<CarTest, int>
{
    /// <summary>
    /// Gets or sets brand Model.
    /// </summary>
    /// <value>
    /// Brand Model.
    /// </value>
    public string Brand { get; set; }

    /// <summary>
    /// Gets or sets car Model.
    /// </summary>
    /// <value>
    /// Car Model.
    /// </value>
    public string Model { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return this.Equals(obj as CarTest);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return System.HashCode.Combine(base.GetHashCode());
    }
}