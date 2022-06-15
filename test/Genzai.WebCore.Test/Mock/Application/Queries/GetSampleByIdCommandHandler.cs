using FluentValidation;
using Genzai.WebCore.Queries;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;

namespace Genzai.WebCore.Test.Mock.Application.Queries
{
    /// <summary>
    /// GetSampleByIdCommandHandler
    /// </summary>
    public class GetSampleByIdCommandHandler : GetEntityByIdQueryHandler<Sample, ISampleRepository, GetSampleByIdRequest, SampleResponse>
    {
        public GetSampleByIdCommandHandler(ISampleRepository repository,
            IValidator<GetEntityByIdQuery<SampleResponse>> validator, IMapper mapper) : base(repository, validator, mapper)
        {
        }
    }
}