using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerUnitTest
    {
        private NetworkMetricsController controller;
        private Mock<INetworkMetricsRepository> mock;
        private DateTimeOffset fromTime = new(new(2020, 01, 01));
        private DateTimeOffset toTime = new(new(2020, 12, 31));

        public NetworkMetricsControllerUnitTest()
        {
            mock = new Mock<INetworkMetricsRepository>();
            controller = new NetworkMetricsController(mock.Object);
        }

        [Fact]
        public void GetNetworkMetrics_ReturnsOk()
        {
            var result = controller.GetNetworkMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
