using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTest
    {
        private RamMetricsController controller;

        public RamMetricsControllerUnitTest()
        {
            controller = new RamMetricsController();
        }

        [Fact]
        public void GetRamMetrics_ReturnsOk()
        {
            var result = controller.GetRamMetrics();
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
