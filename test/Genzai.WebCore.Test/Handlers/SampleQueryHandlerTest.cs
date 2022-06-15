using Genzai.WebCore.Test.Common;
using Genzai.WebCore.Test.Mock.Application.Queries;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Data.Search;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Repositories;
using Xunit;

namespace Genzai.WebCore.Test.Handlers
{
    public class SampleQueryHandlerTest : BaseQueryHandlerTest<ISampleRepository, Sample, SampleSearch, SampleSearchResult>
    {

        public SampleQueryHandlerTest(SampleTestContext testContext) : base(testContext)
        {
            repository = new SampleRepository(context);


        }

        [Theory]
        [ClassData(typeof(GetSampleByIdDataTest))]
        public async Task GetSampleByIdTest(Sample sampleBeforeInsert, long id, bool expectedError)
        {

            GetSampleByIdCommandValidator validator = new GetSampleByIdCommandValidator();
            GetSampleByIdCommandHandler queryHandler = new GetSampleByIdCommandHandler(repository, validator, mapper);
            GetSampleByIdRequest queryRequest = new GetSampleByIdRequest(id);
            await GetEntityByIdTest<GetSampleByIdCommandHandler, GetSampleByIdRequest, SampleResponse>(
                queryHandler, queryRequest, sampleBeforeInsert, expectedError, null);
        }

        [Theory]
        [ClassData(typeof(GetSampleSearchDataTest))]
        public async Task GetSampleSearchTest(SampleSearchRequest searchRequest, bool expectedError,
            int expectedSize, SampleSearchResponse firstExpectedResult)
        {
            GetSampleSearchCommandValidator validator = new GetSampleSearchCommandValidator();
            GetSampleSearchCommandHandler commandHandler = new GetSampleSearchCommandHandler(repository, validator, mapper);

            GetSampleSearchRequest command = new GetSampleSearchRequest(searchRequest);
            await GetEntitySearchTest<GetSampleSearchCommandHandler, GetSampleSearchRequest, SampleSearchRequest, SampleSearchResponse>(
                commandHandler, command, null, expectedError, expectedSize, firstExpectedResult);
        }

    }

    public class GetSampleByIdDataTest : BaseGeneratorTest
    {
        protected override List<object[]> GetData()
        {
            return new List<object[]>
            {
                //Id negativo
                new object[] {
                    null,
                    -1,
                    true
                },
                //Id no existe
                new object[] {
                    null,
                    50,
                    true
                },
                 //Correcto
                new object[] {
                    null,
                    1,
                    true
                },

            };
        }

    }


    public class GetSampleSearchDataTest : BaseGeneratorTest
    {
        protected override List<object[]> GetData()
        {
            return new List<object[]>
            {
                //Numero de pagina incorrectos
                new object[] {
                    new SampleSearchRequest{ PageSize=-1, PageNumber=-1},
                    true,
                    0,
                    null
                },
                //busqueda devuelve 1 elemento
                new object[] {
                    new SampleSearchRequest {
                        SubSampleId = 1,
                        SearchFilter = "sample1",
                    },
                    false,
                    1,
                    new SampleSearchResponse {
                        Id = 1,
                        Name = "sample1",
                        SubSampleId=1,
                    },
                },
                 //Busca devuelve 2 elementos
                new object[] {
                    new SampleSearchRequest {
                        SearchFilter = "sample",
                        PageNumber = 1,
                        PageSize = 3,
                        OrderBy = "name",
                        OrderCriteria = "desc",
                    },
                    false,
                    2,
                    new SampleSearchResponse {
                        Id = 2,
                        Name = "sample2",
                        SubSampleId=2,
                    }
                },
                new object[] {
                    new SampleSearchRequest {
                        SearchFilter = "notfound"
                    },
                    false,
                    null,
                    null
                },
            };
        }

    }
}

