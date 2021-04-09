using MetricsCommon;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DTO.SQL_Settings
{
    public class SQLiteConfig : ISQLiteConfig
    {
        private const string ConnectionString = @"Data Source = metrics.db; Version = 3;";
        private Dictionary<Tables, string> tablesDB;
        private Dictionary<AgentColumns, string> agentColumns;
        public SQLiteConfig()
        {
            tablesDB.Add(Tables.CpuMetrics, "cpumetrics");
            tablesDB.Add(Tables.DotNetMetrics, "dotnetmetrics");
            tablesDB.Add(Tables.HddMetrics, "hddmetrics");
            tablesDB.Add(Tables.NetworkMetrics, "networkmetrics");
            tablesDB.Add(Tables.RamMetrics, "rammetrics");

            agentColumns.Add(AgentColumns.Id, "Id");
            agentColumns.Add(AgentColumns.Time, "Time");
            agentColumns.Add(AgentColumns.Value, "Value");
        }
        public SQLiteConnection SqlConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
