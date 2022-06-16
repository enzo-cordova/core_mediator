using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genzai.EfCore.Repository;
public interface IAuditableRepository<TEntity, TKey> : IRepository<TEntity, TKey>
     where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>

{


    /// <summary>
    /// Save auditable
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SaveAuditableAsync(CancellationToken cancellationToken);

}
