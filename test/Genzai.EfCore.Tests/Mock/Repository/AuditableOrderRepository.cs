namespace Genzai.EfCore.Tests.Mock.Repository;

/// <summary>
/// Auditable Repository
/// </summary>
public class AuditableOrderRepository : AuditableRepository<FakeDomainContext, AuditableOrder, int>, IAuditableOrderRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public AuditableOrderRepository(FakeDomainContext context) : base(context)
    {
    }
}