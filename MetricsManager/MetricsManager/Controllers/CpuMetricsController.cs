using Microsoft.AspNetCore.Mvc;
using System;
using MetricsCommon;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "CpuMetricsController created");
        }

        /// <summary>
        /// Возвращает по запросу метрики использования CPU определенного агента в указанный промежуток времени
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики использования CPU (в процентах)</returns>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace(1, $"Query GetCpuMetrics with params: AgentID={agentId}, FromTime={fromTime}, ToTime={toTime}");
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу метрики использования CPU определенного агента в указанный промежуток времени и заданным перцентилем
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <param name="percentile">Значение перцентиля</param>
        /// <returns>Метрики использования CPU (перцентиль)</returns>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime, Percentile percentile)
        {
            _logger.LogTrace($"Query GetCpuMetrics with params: AgentID={agentId}, FromTime={fromTime}, ToTime={toTime}, Percentile={percentile}");
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу метрики использования CPU всего кластера в указанный промежуток времени
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики использования CPU (в процентах)</returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace($"Query GetCpuMetrics with params: FromTime={fromTime}, ToTime={toTime}");
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу метрики использования CPU всего кластера в указанный промежуток времени и заданным перцентилем
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <param name="percentile">Значение перцентиля</param>
        /// <returns>Метрики использования CPU (перцентиль)</returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogTrace($"Query GetCpuMetrics with params: FromTime={fromTime}, ToTime={toTime}, Percentile={percentile}");
            return Ok();
        }
    }
}
