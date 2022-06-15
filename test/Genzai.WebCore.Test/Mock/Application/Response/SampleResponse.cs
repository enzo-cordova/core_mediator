using Genzai.WebCore.Responses;
using Genzai.WebCore.Test.Mock.Application.Request;

namespace Genzai.WebCore.Test.Mock.Application.Response
{
    /// <summary>
    /// Respuesta de sample
    /// </summary>
    public class SampleResponse : SampleInsertRequest, IEntityResponse
    {
        /// <summary>
        /// The Id
        /// </summary>
        public long? Id { get; set; }


    }
}
