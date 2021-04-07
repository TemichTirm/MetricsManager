using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "HddMetricsController created");
        }
        /// <summary>
        /// Возвращает по запросу оставшееся пространство на HDD определенного агента
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <returns>Оставшееся пространство на HDD</returns>
        [HttpGet("left/agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            _logger.LogTrace($"Query GetHddMetrics with params: AgentID={agentId}");
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу оставшееся пространство на HDD всего кластера
        /// </summary>
        /// <returns>Оставшееся пространство на HDD</returns>
        [HttpGet("left/cluster")]
        public IActionResult GetMetricsFromAllCluster()
        {
            _logger.LogTrace($"Query GetHddMetrics without params");
            return Ok();
        }
    }
}
