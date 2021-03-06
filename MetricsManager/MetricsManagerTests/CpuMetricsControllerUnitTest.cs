using MetricsCommon;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTest
    {
        private CpuMetricsController controller;     
        private int agentId = 1;
        private Mock<ILogger<CpuMetricsController>> mockLogger;
        private TimeSpan fromTime = TimeSpan.FromSeconds(0);
        private TimeSpan toTime = TimeSpan.FromSeconds(100);
        private Percentile percentile = Percentile.P99;

        public CpuMetricsControllerUnitTest()
        {
            mockLogger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(mockLogger.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {  
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPercentileFromAgent_ReturnsOk()
        {
            var result = controller.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, percentile);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPercentileFromAllCluster_ReturnsOk()
        {
            var result = controller.GetMetricsByPercentileFromAllCluster(fromTime, toTime, percentile);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

