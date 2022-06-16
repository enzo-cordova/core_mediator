namespace Genzai.EfCore.Tests.Mock.Repository;

/// <summary>
/// Autable Repository
/// </summary>
public interface IAuditableOrderRepository : IAuditableRepository<AuditableOrder, int>
{
}