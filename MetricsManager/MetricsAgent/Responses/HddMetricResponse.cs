﻿using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class HddMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
    public class SelectByTimePeriodHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
