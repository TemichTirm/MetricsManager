using MetricsAgent.Controllers;
using MetricsCommon;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTest
    {
        private CpuMetricsController controller;
        private TimeSpan fromTime = TimeSpan.FromSeconds(0);
        private TimeSpan toTime = TimeSpan.FromSeconds(100);
        private Percentile percentile = Percentile.P99;

        public CpuMetricsControllerUnitTest()
        {
            controller = new CpuMetricsController();
        }

        [Fact]
        public void GetCpuMetrics_ReturnsOk()
        {
            var result = controller.GetCpuMetrics(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetCpuMetricsByPercentile_ReturnsOk()
        {
            var result = controller.GetCpuMetricsByPercentile(fromTime, toTime, percentile);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
