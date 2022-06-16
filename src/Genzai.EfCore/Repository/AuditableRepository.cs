using Genzai.EfCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genzai.EfCore.Repository;
public abstract class AuditableRepository<TContext, TEntity, TKey>: 
    Repository<TContext, TEntity, TKey>, IAuditableRepository<TEntity, TKey>
    where TContext : ContextDataBase<TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Database Context.
    /// </summary>
    protected readonly TContext context;

    protected AuditableRepository(TContext context) : base(context)
    {
        this.context = context;
    }

    /// <summary>
    /// Save auditable
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> SaveAuditableAsync(CancellationToken cancellationToken)
    {
        return await context.SaveAuditChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
    }
}
