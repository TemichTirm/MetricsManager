using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        public DotNetMetricsController(ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, ".NetMetricsController created");
        }
        /// <summary>
        /// Возвращает по запросу метрики работы .Net определенного агента в указанный промежуток времени
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики работы .Net</returns>
        [HttpGet("errors-count/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace($"Query GetDotNetMetrics with params: AgentID={agentId}, FromTime={fromTime}, ToTime={toTime}");
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу метрики работы .Net всего кластера в указанный промежуток времени
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики работы .Net</returns>
        [HttpGet("errors-count/cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogTrace($"Query GetDotNetMetrics with params: FromTime={fromTime}, ToTime={toTime}");
            return Ok();
        }
    }
}
