using Genzai.WebCore.Requests;

namespace Genzai.WebCore.Test.Mock.Application.Request
{
    /// <summary>
    /// SampleUpdateRequest
    /// </summary>
    public class SampleUpdateRequest : IEntityUpdateRequest
    {

        public string Name { get; set; }
        public long SubSampleId { get; set; }

    }
}