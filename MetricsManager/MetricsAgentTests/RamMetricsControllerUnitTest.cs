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
    public class RamMetricsControllerUnitTest
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mockRepository;
        private Mock<ILogger<RamMetricsController>> mockLogger;
        private DateTimeOffset fromTime = new(new(2020, 01, 01));
        private DateTimeOffset toTime = new(new(2020, 12, 31));

        public RamMetricsControllerUnitTest()
        {
            mockRepository = new Mock<IRamMetricsRepository>();
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(mockRepository.Object, mockLogger.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            double startTime = (fromTime - new DateTime(2000, 01, 01)).TotalSeconds;
            double endTime = (toTime - new DateTime(2000, 01, 01)).TotalSeconds;

            mockRepository.Setup(repository => repository.GetByTimePeriod(startTime, endTime)).Returns(new List<RamMetric>()).Verifiable();
            var result = controller.GetRamMetrics(fromTime, toTime);
            mockRepository.Verify(repository => repository.GetByTimePeriod(startTime, endTime), Times.AtMostOnce());
        }
    }
}
