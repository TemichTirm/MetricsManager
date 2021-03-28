using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerUnitTest
    {
        private NetworkMetricsController controller;
        private TimeSpan fromTime = TimeSpan.FromSeconds(0);
        private TimeSpan toTime = TimeSpan.FromSeconds(100);

        public NetworkMetricsControllerUnitTest()
        {
            controller = new NetworkMetricsController();
        }

        [Fact]
        public void GetNetworkMetrics_ReturnsOk()
        {
            var result = controller.GetNetworkMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
