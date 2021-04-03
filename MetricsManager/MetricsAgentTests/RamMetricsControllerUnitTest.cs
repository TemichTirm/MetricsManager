using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTest
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private DateTimeOffset fromTime = new(new(2020, 01, 01));
        private DateTimeOffset toTime = new(new(2020, 12, 31));

        public RamMetricsControllerUnitTest()
        {
            mock = new Mock<IRamMetricsRepository>();
            controller = new RamMetricsController(mock.Object);
        }

        [Fact]
        public void GetRamMetrics_ReturnsOk()
        {
            var result = controller.GetRamMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
