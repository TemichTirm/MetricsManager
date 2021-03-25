using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram/available")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        /// <summary>
        /// Возвращает по запросу размер доступной оперативной памяти
        /// </summary>
        /// <returns>Размер доступной оперативной памяти</returns>
        [HttpGet]
        public IActionResult GetRamMetrics()
        {
            return Ok();
        }
    }
}
