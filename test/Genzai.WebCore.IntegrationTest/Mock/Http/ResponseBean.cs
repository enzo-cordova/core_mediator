using System.Net;

namespace Genzai.WebCore.Integration.Test.Mock.Http
{
    /// <summary>
    /// Response bean
    /// </summary>
    /// <typeparam name="TReturnObject"></typeparam>
    public class ResponseBean<TReturnObject>
    {
        /// <summary>
        /// Return object
        /// </summary>
        public string? RawResult { get; set; }

        /// <summary>
        /// Return object
        /// </summary>
        public TReturnObject? ReturnObject { get; set; }

        /// <summary>
        /// Status code
        /// </summary>
        public HttpStatusCode? StatusCode { get; set; }
    }
}
