using Genzai.WebCore.Requests;

namespace Genzai.WebCore.Test.Mock.Application.Request
{
    /// <summary>
    /// SampleInsertRequest
    /// </summary>
    public class SampleInsertRequest : IEntityInsertRequest
    {
        public string Name { get; set; }
        public long SubSampleId { get; set; }
    }

}