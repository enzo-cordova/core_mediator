using Genzai.Core.Domain.Model.KeyLess;

namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Entity Without primary key
/// </summary>
[Keyless]
public class CarBaseTest : EntityBase<CarBaseTest>
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
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode());
    }
}