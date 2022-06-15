using FluentValidation;
using Genzai.WebCore.Queries;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Data.Search;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;

namespace Genzai.WebCore.Test.Mock.Application.Queries
{
    /// <summary>
    /// GetSampleSearchCommandHandler
    /// </summary>
    public class GetSampleSearchCommandHandler : GetEntitySearchQueryHandler<Sample, SampleSearch, SampleSearchResult,
        ISampleRepository, SampleSearchRequest, GetSampleSearchRequest, SampleSearchResponse>
    {
        public GetSampleSearchCommandHandler(ISampleRepository repository, IValidator<GetEntitySearchQuery<SampleSearchRequest, SampleSearchResponse>> validator, IMapper mapper) : base(repository, validator, mapper)
        {
        }



    }
}