using MetricsAgent.DTO;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/hddMetrics/left")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private IHddMetricsRepository _repository;
        private readonly DateTimeOffset baseTime = new (new(2000, 01, 01));
        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "HddMetricsController created");
            _repository = repository;
        }
        /// <summary>
        /// Возвращает по запросу оставшееся пространство на HDD в указанный промежуток времени
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Оставшееся пространство на HDD</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetHddMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogTrace(1, $"Query GetHddMetrics with params: FromTime={fromTime}, ToTime={toTime}");
            var metrics = _repository.GetByTimePeriod((fromTime - baseTime).TotalSeconds, (toTime - baseTime).TotalSeconds);
            var response = new SelectByTimePeriodHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto
                {
                    Time = baseTime.AddSeconds(metric.Time),
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            return Ok(response);
        }
    }
}
