using AutoMapper;
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
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, 
                                    IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "CpuMetricsController created");
            _repository = repository;
            _mapper = mapper;
        }
        

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogTrace(1, $"Query Create Metric with params: Value={request.Value}, Time={request.Time}");
            _repository.Create(_mapper.Map<CpuMetric>(request));
            return Ok();
        }
        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogTrace(1, $"Query GetAll Metrics without params");
            var metrics = _repository.GetAll();
            var response = new AllCpuMetricsResponse() { Metrics = new List<CpuMetricDto>() };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Ok(response);
        }
        
        /// <summary>
        /// Возвращает по запросу метрики загрузки CPU в указанный промежуток времени
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики использования CPU (в процентах)</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogTrace(1, $"Query GetCpuMetrics with params: FromTime={fromTime}, ToTime={toTime}");
            var metrics = _repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());
            var response = new SelectByTimePeriodCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
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
            var orderedMetrics = _repository.GetByTimePeriod(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds())
                                     .OrderBy(metrics => metrics.Value);
            if (!orderedMetrics.Any())
            { 
                return null; 
            }
            int index = 0;
            switch (percentile)
            {
                case Percentile.Median:
                    index = (int)(orderedMetrics.Count() / 2);
                    break;
                case Percentile.P75:
                    index = (int)(orderedMetrics.Count() * 0.75);
                    break;
                case Percentile.P90:
                    index = (int)(orderedMetrics.Count() * 0.90);
                    break;
                case Percentile.P95:
                    index = (int)(orderedMetrics.Count() * 0.95);
                    break;
                case Percentile.P99:
                    index = (int)(orderedMetrics.Count() * 0.99);
                    break;
            }
            var response = _mapper.Map<CpuMetricDto>(orderedMetrics.ElementAt(index));            
            return Ok(response);
        }
    }
  

}
