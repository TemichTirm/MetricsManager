using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd/left")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        /// <summary>
        /// Возвращает по запросу оставшееся пространство на HDD
        /// </summary>
        /// <returns>Оставшееся пространство на HDD</returns>
        [HttpGet]
        public IActionResult GetHddMetrics()
        {
            return Ok();
        }
    }
}
