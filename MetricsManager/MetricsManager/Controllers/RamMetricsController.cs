using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        /// <summary>
        /// Возвращает по запросу размер доступной оперативной памяти определенного агента
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <returns>Размер доступной оперативной памяти</returns>
        [HttpGet("available/agent/{agentId}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            return Ok();
        }

        /// <summary>
        /// Возвращает по запросу размер доступной оперативной памяти всего кластера
        /// </summary>
        /// <returns>Размер доступной оперативной памяти</returns>
        [HttpGet("available/cluster")]
        public IActionResult GetMetricsFromAllCluster()
        {
            return Ok();
        }
    }
}
