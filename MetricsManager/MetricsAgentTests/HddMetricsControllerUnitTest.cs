using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class HddMetricsControllerUnitTest
    {
        private HddMetricsController controller;

        public HddMetricsControllerUnitTest()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetHddMetrics_ReturnsOk()
        {
            var result = controller.GetHddMetrics();
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
