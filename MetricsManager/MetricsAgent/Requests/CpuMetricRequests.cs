using System;

namespace MetricsAgent.Requests
{
    public class CpuMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
    public class CpuMetricUpdateRequest
    {
        public int Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
    public class CpuMetricDeleteRequest
    {
        public int Id { get; set; }
    }
}
