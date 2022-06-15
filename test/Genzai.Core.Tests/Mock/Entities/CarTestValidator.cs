namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Car Test Validator
/// </summary>
public class CarTestValidator : AbstractValidator<CarTest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarTestValidator"/> class.
    /// </summary>
    public CarTestValidator()
    {
        this.RuleFor(p => p.Brand).NotNull();
        this.RuleFor(p => p.Model).NotNull();
    }
}