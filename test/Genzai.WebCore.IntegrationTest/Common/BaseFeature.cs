using System.Collections.Generic;
using System.Net.Http;
using Xunit.Abstractions;

namespace Genzai.WebCore.Integration.Test.Common
{
    public abstract class BaseFeature
    {


        private static readonly Dictionary<string, HttpClient> clientByUserId = new();
        private static readonly object lockObject = new();

        protected readonly ITestOutputHelper _output;

        protected BaseFeature(ITestOutputHelper output)
        {
            this._output = output;
        }


        protected static HttpClient GetClient()
        {
            return WebCoreIntegrationTestStartup.Instance.getClient();
        }


    }
}
