using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Data.Search;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;

namespace Genzai.WebCore.Test.Mock.Application.AutoMapper
{
    public class SampleMapping : Profile
    {
        public SampleMapping()
        {
            //Sample
            CreateMap<SampleSearchRequest, SampleSearch>();
            CreateMap<SampleSearchResult, SampleSearchResponse>();
            CreateMap<SampleInsertRequest, Sample>();
            CreateMap<SampleUpdateRequest, Sample>();
            CreateMap<SampleInsertRequest, SampleResponse>();

            this.CreateMap<Sample, SampleResponse>();

        }
    }
}
