using MetricsAgent.DTO;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/dotnetMetrics/errors-count")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;
        private readonly DateTimeOffset baseTime = new (new(2000, 01, 01));
        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, ".NetMetricsController created");
            _repository = repository;
        }
        /// <summary>
        /// Возвращает по запросу метрики работы .Net в указанный промежуток времени
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики работы .Net</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogTrace(1, $"Query GetDotNetMetrics with params: FromTime={fromTime}, ToTime={toTime}");
            var metrics = _repository.GetByTimePeriod((fromTime - baseTime).TotalSeconds, (toTime - baseTime).TotalSeconds);
            var response = new SelectByTimePeriodDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto { Time = baseTime.AddSeconds(metric.Time),
                                                           Value = metric.Value, 
                                                           Id = metric.Id });
            }
            return Ok(response);
        }
    }
}
