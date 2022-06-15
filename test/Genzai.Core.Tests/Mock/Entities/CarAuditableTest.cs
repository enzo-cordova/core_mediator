﻿namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Car Test Entity.
/// </summary>
public class CarAuditableTest : AuditableEntity<CarAuditableTest, int>, IAuditable
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
        return Equals(obj as CarAuditableTest);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode());
    }
}