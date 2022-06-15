using Genzai.WebCore.Test.Common;
using Genzai.WebCore.Test.Mock.Application.Commands.Delete;
using Genzai.WebCore.Test.Mock.Application.Commands.Insert;
using Genzai.WebCore.Test.Mock.Application.Commands.Updates;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Data.Search;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Genzai.WebCore.Test.Handlers
{
    public class SampleCommandHandlerTest : BaseCommandHandlerTest<ISampleRepository, Sample, SampleSearch, SampleSearchResult>
    {

        public SampleCommandHandlerTest(SampleTestContext testContext) : base(testContext)
        {
            repository = new SampleRepository(context);


        }

        [Theory]
        [ClassData(typeof(InsertSampleDataTest))]
        public async Task InsertSampleTest(Sample sampleBeforeInsert, SampleInsertRequest insertRequest, bool expectedError)
        {

            ILogger<InsertSampleCommand> logger = new Mock<ILogger<InsertSampleCommand>>().Object;
            InsertSampleCommandValidator validator = new InsertSampleCommandValidator();
            InsertSampleCommandHandler commandHandler = new InsertSampleCommandHandler(repository, logger, validator, mapper);
            InsertSampleCommand command = new InsertSampleCommand(insertRequest);
            await InsertEntityTest<InsertSampleCommandHandler, InsertSampleCommand, SampleInsertRequest, SampleResponse>(
                commandHandler, command, sampleBeforeInsert, expectedError, null);
        }

        [Theory]
        [ClassData(typeof(UpdateSampleDataTest))]
        public async Task UpdateSampleTest(Sample sampleBeforeUpdate, SampleUpdateRequest updateRequest, bool expectedError)
        {
            ILogger<UpdateSampleCommand> logger = new Mock<ILogger<UpdateSampleCommand>>().Object;
            UpdateSampleCommandValidator validator = new UpdateSampleCommandValidator();
            UpdateSampleCommandHandler commandHandler = new UpdateSampleCommandHandler(repository, validator, mapper);

            UpdateSampleCommand command = new UpdateSampleCommand(589, updateRequest);
            await UpdateEntityTest<UpdateSampleCommandHandler, UpdateSampleCommand, SampleUpdateRequest>(
                commandHandler, command, sampleBeforeUpdate, expectedError);
        }

        [Theory]
        [ClassData(typeof(DeleteSampleDataTest))]
        public async Task DeleteSampleTest(Sample sampleBeforeDelete, int id, bool expectedError)
        {
            DeleteSampleCommandValidator validator = new DeleteSampleCommandValidator();
            DeleteSampleCommandHandler commandHandler = new DeleteSampleCommandHandler(repository, validator);

            DeleteSampleCommand command = new DeleteSampleCommand(id);
            await DeleteEntityTest(
                commandHandler, command, sampleBeforeDelete, expectedError);
        }
    }


    public class InsertSampleDataTest : BaseGeneratorTest
    {
        protected override List<object[]> GetData()
        {
            return new List<object[]>
            {
                //sin ningun campo
                new object[] {
                    null,
                    new SampleInsertRequest {  },
                    true
                },
                //SubSampleId incorrecto
                new object[] {
                    null,
                    new SampleInsertRequest {
                        Name = "eeeee",
                        SubSampleId = 0
                    },
                    true
                },
                
                //Correcto
                new object[] {
                    null,
                    new SampleInsertRequest {
                       Name = "eeeee",
                       SubSampleId = 1
                    },
                    false
                },
            };
        }

    }

    public class UpdateSampleDataTest : BaseGeneratorTest
    {
        protected override List<object[]> GetData()
        {
            Sample sampleToCreate = new Sample()
            {
                Name = "sample1",
                SubSampleId = 2,
            };
            return new List<object[]>
            {
                //sin ningun campo
                new object[] {
                    null,
                    new SampleUpdateRequest {  },
                    true
                },
                //SubSampleId incorrecto
                new object[] {
                    null,
                    new SampleUpdateRequest {
                        Name = "nameUpdated",
                        SubSampleId = -1,
                    },
                    true
                },
                 
                //Correcto
                new object[] {
                    sampleToCreate,
                    new SampleUpdateRequest {
                        Name = "eeeee",
                        SubSampleId = 2,
                    },
                    false
                },
            };
        }
    }


    public class DeleteSampleDataTest : BaseGeneratorTest
    {
        protected override List<object[]> GetData()
        {
            Sample sampleToCreate = new Sample()
            {
                Name = "sample1",
                SubSampleId = 2,
            };
            return new List<object[]>
            {
                //id invalido
                new object[] {
                    null,
                    -1,
                    true
                },
                //Id no existe
                new object[] {
                    null,
                    2845,
                    true
                },
                //Id no existe
                new object[] {
                    sampleToCreate,
                    null,
                    true
                },
            };
        }
    }
}

