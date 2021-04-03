using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTest
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private DateTimeOffset fromTime = new(new(2020, 01, 01));
        private DateTimeOffset toTime = new(new(2020, 12, 31));

        public DotNetMetricsControllerUnitTest()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            controller = new DotNetMetricsController(mock.Object);
        }

        [Fact]
        public void GetDotNetMetrics_ReturnsOk()
        {
            var result = controller.GetDotNetMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
