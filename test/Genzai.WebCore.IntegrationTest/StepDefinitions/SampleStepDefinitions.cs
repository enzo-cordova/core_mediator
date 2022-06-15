using Genzai.WebCore.Integration.Test.Common;
using Genzai.WebCore.Test.Mock.Application.Response;
using System.Net;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;
using Endpoint = Genzai.WebCore.Integration.Test.Common.Endpoint;

namespace Genzai.WebCore.Integration.Test.StepDefinitions
{
    [Binding]
    public sealed class SampleStepDefinitions : BaseCrudFeature
    {
        private SampleResponse? sampleResponse;
        private string? sampleJson;
        private HttpStatusCode deleteCode;

        public SampleStepDefinitions(ITestOutputHelper output) : base(output)
        {
        }

        [Given("Un sample con los datos:")]
        public void GivenSample(Table table)
        {
            this.sampleJson = this.ParseDataTableToJson(table);
        }

        [When(@"Se envIa sample al API Rest")]
        public void SendSample()
        {
            GetAll("/sample/v1/samples/filter");
            this.sampleResponse = this.Create<SampleResponse>(new Endpoint("/sample/v1/samples"), this.sampleJson!).ReturnObject;
        }

        [Then(@"Se crea sample con los datos:")]
        public void CheckData(Table dataTable)
        {
            ValidateResponse(this.sampleResponse!, convertDatatableToDictionary(dataTable));
        }

        [Then(@"Se borra sample")]
        public void Delete()
        {
            this.deleteCode = Delete(new Endpoint("/sample/v1/samples"), this.sampleResponse!.Id ?? -1);
        }

        [Then(@"La respuesta HTTP sample es OK")]
        public void CheckDelete()
        {
            Assert.Equal(HttpStatusCode.NoContent, this.deleteCode);
        }
    }
}