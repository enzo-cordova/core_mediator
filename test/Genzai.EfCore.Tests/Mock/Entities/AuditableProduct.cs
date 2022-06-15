namespace Genzai.EfCore.Tests.Mock.Entities;

/// <summary>
/// Auditable Product
/// </summary>
public class AuditableProduct : AuditableEntity<AuditableProduct, int>, IAuditable
{
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; }
}