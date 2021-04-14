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
    public class NetworkMetricsControllerUnitTest
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<INetworkMetricsRepository> _mockRepository;
        private readonly Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DateTimeOffset fromTime = new(new(2020, 01, 01));
        private readonly DateTimeOffset toTime = new(new(2020, 12, 31));

        public NetworkMetricsControllerUnitTest()
        {
            _mockRepository = new Mock<INetworkMetricsRepository>();
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new NetworkMetricsController(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => 
                                 repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds())).
                                 Returns(new List<NetworkMetric>()).Verifiable();
            var result = _controller.GetNetworkMetrics(fromTime, toTime);
            _mockRepository.Verify(repository => 
                                  repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds()), 
                                  Times.AtMostOnce());
        }
    }
}
