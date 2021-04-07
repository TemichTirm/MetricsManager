using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTest
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mockRepository;
        private Mock<ILogger<DotNetMetricsController>> mockLogger;
        private DateTimeOffset fromTime = new(new(2020, 01, 01));
        private DateTimeOffset toTime = new(new(2020, 12, 31));

        public DotNetMetricsControllerUnitTest()
        {
            mockRepository = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(mockRepository.Object, mockLogger.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            double startTime = (fromTime - new DateTime(2000, 01, 01)).TotalSeconds;
            double endTime = (toTime - new DateTime(2000, 01, 01)).TotalSeconds;

            mockRepository.Setup(repository => repository.GetByTimePeriod(startTime, endTime)).Returns(new List<DotNetMetric>()).Verifiable();
            var result = controller.GetDotNetMetrics(fromTime, toTime);
            mockRepository.Verify(repository => repository.GetByTimePeriod(startTime, endTime), Times.AtMostOnce());
        }
    }
}
