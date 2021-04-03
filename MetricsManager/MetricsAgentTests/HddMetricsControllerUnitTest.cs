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
    public class HddMetricsControllerUnitTest
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mockRepository;
        private Mock<ILogger<HddMetricsController>> mockLogger;
        private DateTimeOffset fromTime = new(new(2020, 01, 01));
        private DateTimeOffset toTime = new(new(2020, 12, 31));

        public HddMetricsControllerUnitTest()
        {
            mockRepository = new Mock<IHddMetricsRepository>();
            mockLogger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mockRepository.Object, mockLogger.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            double startTime = (fromTime - new DateTime(2000, 01, 01)).TotalSeconds;
            double endTime = (toTime - new DateTime(2000, 01, 01)).TotalSeconds;

            mockRepository.Setup(repository => repository.GetByTimePeriod(startTime, endTime)).Returns(new List<HddMetric>()).Verifiable();
            var result = controller.GetHddMetrics(fromTime, toTime);
            mockRepository.Verify(repository => repository.GetByTimePeriod(startTime, endTime), Times.AtMostOnce());
        }
    }
}
