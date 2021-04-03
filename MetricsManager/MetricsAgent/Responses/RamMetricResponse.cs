using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class RamMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
    public class SelectByTimePeriodRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
