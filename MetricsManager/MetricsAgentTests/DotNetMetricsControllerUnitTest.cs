using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using MetricsAgent.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTest
    {
        private readonly DotNetMetricsController _controller;
        private readonly Mock<IDotNetMetricsRepository> _mockRepository;
        private readonly Mock<ILogger<DotNetMetricsController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DateTimeOffset fromTime = new(new(2020, 01, 01));
        private readonly DateTimeOffset toTime = new(new(2020, 12, 31));

        public DotNetMetricsControllerUnitTest()
        {
            _mockRepository = new Mock<IDotNetMetricsRepository>();
            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new DotNetMetricsController(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => 
                                  repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds()))
                                  .Returns(new List<DotNetMetric>()).Verifiable();
            var result = _controller.GetDotNetMetrics(fromTime, toTime);
            _mockRepository.Verify(repository =>
                                   repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds()), 
                                   Times.AtMostOnce());
        }
    }
}
