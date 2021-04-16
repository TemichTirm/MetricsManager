using Dapper;
using MetricsAgent.Models;
using MetricsCommon.SQL_Settings;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DTO
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        public void Create(NetworkMetric item)
        {
            try
            {
                using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
                {
                    connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new { value = item.Value, time = item.Time });
                }
            }
            catch (Exception) { }
        }
        public IList<NetworkMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();
            }
        }
        public IList<NetworkMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
