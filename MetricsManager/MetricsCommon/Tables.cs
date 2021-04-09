
namespace MetricsCommon
{
    public enum Tables
    {
        CpuMetrics,
        DotNetMetrics,
        HddMetrics,
        NetworkMetrics,
        RamMetrics
    }
    public enum AgentColumns
    {
        Id,
        Value,
        Time
    }
    public enum ManagerColumns
    {
        Id,
        AgentId,
        Value,
        Time
    }
}
