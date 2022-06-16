using Genzai.EfCore.Repository;
using Genzai.WebCore.Test.Mock.Domain.Data.Search;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;

namespace Genzai.WebCore.Test.Mock.Domain.Repositories
{
    public interface ISampleRepository : IPartialSearchRepository<Sample, long, SampleSearch, SampleSearchResult>
    {
        Task<bool> SaveAuditableAsync(CancellationToken cancellationToken);
    }
}
