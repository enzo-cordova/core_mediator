using Genzai.EfCore.Repository;
using Genzai.EfCore.Utils;
using Genzai.WebCore.Test.Mock.Domain.Data.Search;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Context;
using LinqKit;

namespace Genzai.WebCore.Test.Mock.Infrastructure.Data.Repositories
{
    public class SampleRepository : PartialSearchRepository<SampleContext, Sample, long, SampleSearch, SampleSearchResult>, ISampleRepository
    {

        public SampleRepository(SampleContext context) : base(context)
        {

        }


        protected override IQueryable<SampleSearchResult> InitQuery(SampleSearch search)
        {
            return from sample in this.GetEntityDbSet()
                       //Result
                   select new SampleSearchResult
                   {
                       Name = sample.Name,
                       Id = sample.Id,
                       SubSampleId = sample.SubSampleId
                   };
        }

        protected override void AppendConditions(ref ExpressionStarter<SampleSearchResult> queryExpression, SampleSearch search)
        {
            QueryUtils.AppendConditionNotNull(ref queryExpression, nameof(SampleSearchResult.Name), QueryUtils.And);
            QueryUtils.AppendConditionNull(ref queryExpression, nameof(SampleSearchResult.Name), QueryUtils.AndNot);
            QueryUtils.AppendConditionEquals(ref queryExpression, nameof(SampleSearchResult.SubSampleId), search.SubSampleId, QueryUtils.And);
            if (search.SubSampleId != null)
            {
                QueryUtils.AppendConditionList(ref queryExpression, nameof(SampleSearchResult.SubSampleId), new List<long?> { search.SubSampleId }, QueryUtils.And);
                QueryUtils.AppendConditionRange(ref queryExpression, nameof(SampleSearchResult.SubSampleId), search.SubSampleId, search.SubSampleId, QueryUtils.And);
                QueryUtils.AppendConditionStringStartsWith(ref queryExpression, nameof(SampleSearchResult.Name), search.SearchFilter, QueryUtils.And);

            }
            this.AppendFilterConditions(ref queryExpression, search, new List<string> {
                nameof(SampleSearchResult.Name)
            });
        }



    }
}
