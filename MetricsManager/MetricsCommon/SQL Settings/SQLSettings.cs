using System.Collections.Generic;

namespace MetricsCommon.SQL_Settings
{
    public enum Tables
    {
        CpuMetrics,
        DotNetMetrics,
        HddMetrics,
        NetworkMetrics,
        RamMetrics
    }
    public enum AgentFields
    {
        Id,
        Value,
        Time
    }
    public enum ManagerFields
    {
        Id,
        AgentId,
        Value,
        Time
    }
    public class SQLSettings : ISQLSettings
    {
        private static readonly string connectionString = @"Data Source=metrics.db; Version=3;";
        private readonly Dictionary<Tables, string> tablesDB = new()
        {
            { Tables.CpuMetrics, "cpumetrics" },
            { Tables.DotNetMetrics, "dotnetmetrics" },
            { Tables.HddMetrics, "hddmetrics" },
            { Tables.NetworkMetrics, "networkmetrics" },
            { Tables.RamMetrics, "rammetrics" },
        };
        private readonly Dictionary<AgentFields, string> agentFields = new()
        {
            { AgentFields.Id, "Id" },
            { AgentFields.Time, "Time" },
            { AgentFields.Value, "Value" },
        };
        private readonly Dictionary<ManagerFields, string> managerFields = new()
        {
            { ManagerFields.Id, "Id" },
            { ManagerFields.AgentId, "AgentId" },
            { ManagerFields.Time, "Time" },
            { ManagerFields.Value, "Value" },
        };
        public static string ConnectionString
        {
            get { return connectionString; }
        }
        public string this[Tables key]
        {
            get { return tablesDB [key]; }
        }
        public string this[AgentFields key]
        {
            get { return agentFields[key]; }
        }
        public string this[ManagerFields key]
        {
            get { return managerFields[key]; }
        }
    }
}
