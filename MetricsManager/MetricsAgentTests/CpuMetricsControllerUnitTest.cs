using AutoFixture;
using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using MetricsAgent.Models;
using MetricsAgent.Responses;
using MetricsCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTest
    {
        private readonly CpuMetricsController _controller;
        private readonly Mock<ICpuMetricsRepository> _mockRepository;
        private readonly Mock<ILogger<CpuMetricsController>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly List<CpuMetric> _initialData;
        private readonly DateTimeOffset fromTime = new(new(2020, 01, 01));
        private readonly DateTimeOffset toTime = new(new(2020, 12, 31));
        private readonly Percentile percentile = Percentile.P75;

        public CpuMetricsControllerUnitTest()
        {
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mapper = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile())).CreateMapper();
            _controller = new CpuMetricsController(_mockRepository.Object, _mockLogger.Object, _mapper);
            _initialData = new Fixture().Create<List<CpuMetric>>();
        }

        [Fact]
        public void GetAll_ShouldCall_GetAll_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetAll()).Returns(new List<CpuMetric>()).Verifiable();
            var result = _controller.GetAll();
            _mockRepository.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds()))
                                .Returns(_initialData).Verifiable();
            var result = (OkObjectResult)_controller.GetCpuMetrics(fromTime, toTime);
            var actualResult = ((SelectByTimePeriodCpuMetricsResponse)result.Value).Metrics;
            _mockRepository.Verify(repository => repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds())
                                    ,Times.AtMostOnce());
            for (int i = 0; i < _initialData.Count; i++)
            {
                Assert.Equal(_initialData[i].Value, actualResult[i].Value);
                Assert.Equal(_initialData[i].Id, actualResult[i].Id);
                Assert.Equal(_initialData[i].Time, actualResult[i].Time.ToUnixTimeSeconds());

            }
        }

        [Fact]
        public void GetCpuMetricsByPercentile__ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds()))
                                .Returns(_initialData).Verifiable();
            var result = _controller.GetCpuMetricsByPercentile(fromTime, toTime, percentile);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
