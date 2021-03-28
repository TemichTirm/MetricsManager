using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsControllerUnitTest
    {
        private DotNetMetricsController controller;
        private int agentId = 1;
        private TimeSpan fromTime = TimeSpan.FromSeconds(0);
        private TimeSpan toTime = TimeSpan.FromSeconds(100);

        public DotNetMetricsControllerUnitTest()
        {
            controller = new DotNetMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
