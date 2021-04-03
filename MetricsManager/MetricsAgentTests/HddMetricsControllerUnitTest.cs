using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTest
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private DateTimeOffset fromTime = new(new(2020, 01, 01));
        private DateTimeOffset toTime = new(new(2020, 12, 31));

        public HddMetricsControllerUnitTest()
        {
            mock = new Mock<IHddMetricsRepository>();
            controller = new HddMetricsController(mock.Object);
        }

        [Fact]
        public void GetHddMetrics_ReturnsOk()
        {
            var result = controller.GetHddMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
