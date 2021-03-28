using Microsoft.AspNetCore.Mvc;
using System;


namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet/errors-count")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        /// <summary>
        /// Возвращает по запросу метрики работы .Net в указанный промежуток времени
        /// </summary>
        /// <param name="fromTime">Начальное время</param>
        /// <param name="toTime">Конечное время</param>
        /// <returns>Метрики работы .Net</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
