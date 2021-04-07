using MetricsAgent.DTO;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsAgent.Controllers
{
    [Route("api/CpuMetrics")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository _repository;
        private readonly DateTimeOffset baseTime = new (new(2000, 01, 01));

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "CpuMetricsController created");
            _repository = repository;            
        }
        

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogTrace(1, $"Query Create Metric with params: Value={request.Value}, Time={request.Time}");
            _repository.Create(new CpuMetric
            {
                Time = (request.Time - baseTime).TotalSeconds,
                Value = request.Value
            });
            return Ok();
        }
        [HttpPut("update")]
        public IActionResult Update([FromBody] CpuMetricUpdateRequest request)
        {
            _logger.LogTrace(1, $"Query Update Metric with params: ID={request.Id}, Value={request.Value}, Time={request.Time}");
            CpuMetric itemForUpdate = new() { 
                Id = request.Id, 
                Time = (request.Time - baseTime).TotalSeconds, 
                Value = request.Value 
            };
            _repository.Update(itemForUpdate);
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] CpuMetricDeleteRequest request)
        {
            _logger.LogTrace(1, $"Query Delete Metric with params: ID={request.Id}");
            _repository.Delete(request.Id);
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogTrace(1, $"Query GetAll Metrics without params");
            var metrics = _repository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = baseTime.AddSeconds(metric.Time), 
                                                        Value = metric.Value, 
                                                        Id = metric.Id });
            }
            return Ok(response);
        }
        [HttpGet("getmetric/{id}")]
        public IActionResult GetById([FromQuery] int id)
        {
            _logger.LogTrace(1, $"Query GetByID Metrics with params: ID={id}");
            CpuMetric metric = _repository.GetById(id);
            var response = new CpuMetricDto();
            response.Time = baseTime.AddSeconds(metric.Time);
            response.Value = metric.Value;
            response.Id = metric.Id;
            return Ok(response);
        }

        /// <summary>
        /// Возвращает по запросу метрики использования CPU в указанный промежуток времени
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики использования CPU (в процентах)</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogTrace(1, $"Query GetCpuMetrics with params: FromTime={fromTime}, ToTime={toTime}");
            var metrics = _repository.GetByTimePeriod((fromTime - baseTime).TotalSeconds, (toTime - baseTime).TotalSeconds);
            var response = new SelectByTimePeriodCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = baseTime.AddSeconds(metric.Time),
                                                        Value = metric.Value, 
                                                        Id = metric.Id });
            }
            return Ok(response);
        }

        /// <summary>
        /// Возвращает по запросу метрики использования CPU в указанный промежуток времени и заданным перцентилем
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <param name="percentile">Значение перцентиля</param>
        /// <returns>Метрики использования CPU (перцентиль)</returns>
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetCpuMetricsByPercentile([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogTrace($"Query GetCpuMetrics with params: FromTime={fromTime}, ToTime={toTime}, Percentile={percentile}");
            return Ok();
        }
    }
  

}
