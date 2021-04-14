using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DTO;
using MetricsAgent.Models;
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
        private readonly Mock<IMapper> _mockMapper;
        private readonly DateTimeOffset fromTime = new(new(2020, 01, 01));
        private readonly DateTimeOffset toTime = new(new(2020, 12, 31));
        private readonly Percentile percentile = Percentile.P99;

        public CpuMetricsControllerUnitTest()
        {
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = new (new(2020,02,04)), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Update_ShouldCall_Update_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Update(It.IsAny<CpuMetric>())).Verifiable();
            var result = _controller.Update(new MetricsAgent.Requests.CpuMetricUpdateRequest { Id = 3, Time = new(new(2020, 02, 04)), Value = 50 });
            _mockRepository.Verify(repository => repository.Update(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void Delete_ShouldCall_Delete_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Delete(It.IsAny<int>())).Verifiable();
            var result = _controller.Delete(new MetricsAgent.Requests.CpuMetricDeleteRequest { Id = 3});
            _mockRepository.Verify(repository => repository.Delete(It.IsAny<int>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAll_ShouldCall_GetAll_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetAll()).Returns(new List<CpuMetric>()).Verifiable();
            var result = _controller.GetAll();
            _mockRepository.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
        }

        [Fact]
        public void GetByID_ShouldCall_GetByID_From_Repository()
        {
            int id = 1;
            _mockRepository.Setup(repository => repository.GetById(id)).Returns(new CpuMetric()).Verifiable();
            var result = _controller.GetById(id);
            _mockRepository.Verify(repository => repository.GetById(id), Times.AtLeastOnce());
        }

        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds()))
                                .Returns(new List<CpuMetric>()).Verifiable();
            var result = _controller.GetCpuMetrics(fromTime, toTime);
            _mockRepository.Verify(repository => repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds())
                                    , Times.AtMostOnce());
        }

        [Fact]
        public void GetCpuMetricsByPercentile_ReturnsOk()
        {
            var result = _controller.GetCpuMetricsByPercentile(fromTime, toTime, percentile);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
