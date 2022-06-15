namespace Genzai.EfCore.Tests.Mock.Entities;

/// <summary>
/// Product entity.
/// </summary>
public class Product : Entity<Product, int>
{
    /// <summary>
    /// Product description.
    /// </summary>
    public string Description { get; set; }
}