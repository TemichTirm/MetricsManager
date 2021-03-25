using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        /// <summary>
        /// Возвращает по запросу оставшееся пространство на HDD определенного агента
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <returns>Оставшееся пространство на HDD</returns>
        [HttpGet("left/agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу оставшееся пространство на HDD всего кластера
        /// </summary>
        /// <returns>Оставшееся пространство на HDD</returns>
        [HttpGet("left/cluster")]
        public IActionResult GetMetricsFromAllCluster()
        {
            return Ok();
        }
    }
}
