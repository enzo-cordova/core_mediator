using Genzai.EfCore.Search;

namespace Genzai.WebCore.Test.Mock.Domain.Data.Search
{
    public class BaseSampleSearchResult : EntityIdLongSearchResult
    {


        public string Name { get; set; }

        public long SubSampleId { get; set; }
    }
}
