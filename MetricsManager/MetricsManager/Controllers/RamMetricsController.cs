using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "RamMetricsController created");
        }
        /// <summary>
        /// Возвращает по запросу размер доступной оперативной памяти определенного агента
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <returns>Размер доступной оперативной памяти</returns>
        [HttpGet("available/agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            _logger.LogTrace($"Query GetRamMetrics with params: AgentID={agentId}");
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу размер доступной оперативной памяти всего кластера
        /// </summary>
        /// <returns>Размер доступной оперативной памяти</returns>
        [HttpGet("available/cluster")]
        public IActionResult GetMetricsFromAllCluster()
        {
            _logger.LogTrace($"Query GetRamMetrics without params");
            return Ok();
        }
    }
}
