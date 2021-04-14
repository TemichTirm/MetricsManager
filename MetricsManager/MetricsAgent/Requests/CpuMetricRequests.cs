using System;

namespace MetricsAgent.Requests
{
    public class CpuMetricCreateRequest
    {
        public int Value { get; set; }
        private DateTimeOffset time;
        public DateTimeOffset Time 
        {
            get 
            { 
                return time; 
            }
            set 
            { 
                time = new DateTimeOffset(value.DateTime, TimeSpan.FromHours(0)); 
            } 
        }
    }
    public class CpuMetricUpdateRequest
    {
        public int Id { get; set; }
        public int Value { get; set; }
        private DateTimeOffset time;
        public DateTimeOffset Time
        {
            get
            {
                return time;
            }
            set
            {
                time = new DateTimeOffset(value.DateTime, TimeSpan.FromHours(0));
            }
        }
    }
    public class CpuMetricDeleteRequest
    {
        public int Id { get; set; }
    }
}
