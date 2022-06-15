using Genzai.Core.Telemetry;
using Microsoft.ApplicationInsights;

namespace Genzai.Core.Tests.Telemetry
{

    /// <summary>
    /// TelemetryProviderTest
    /// </summary>
    public class TelemetryProviderTest
    {
        private readonly TelemetryClient _telemetryClient; 
        /// <summary>
        /// TelemetryProvider
        /// </summary>
        public TelemetryProviderTest()
        {
            _telemetryClient = new TelemetryClient();

            _telemetryClient.InstrumentationKey = Guid.NewGuid().ToString();
        }

        [Fact]
        public void TelemetryProviderTest_TractEvent()
        {

          // _mockTelemetryClient.Setup(s => s.TrackEvent(It.IsAny<string>(), null, null));

            var telemetryProvider = new TelemetryProvider(_telemetryClient);

            telemetryProvider.TrackEvent(It.IsAny<string>());
            Assert.NotNull(telemetryProvider);
        }

        [Fact]
        public void TelemetryProviderTest_AddPropertyPassValue_returnOk()
        {
            var telemetryProvider = new TelemetryProvider(_telemetryClient);
            telemetryProvider.AddEventProperty("camera", "true");

            Assert.NotNull(telemetryProvider);
        }
        [Fact]
        public void TelemetryProviderTest_AddProperty_PassNull()
        {
            var telemetryProvider = new TelemetryProvider(_telemetryClient);
            telemetryProvider.AddEventProperty(It.IsAny<string>(), It.IsAny<string>());

            Assert.NotNull(telemetryProvider);
        }

    }
}
