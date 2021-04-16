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
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
    }
    public class HddMetricsRepository : IHddMetricsRepository
    {
        public void Create(HddMetric item)
        {
            try
            {
                using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
                {
                    connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                    new { value = item.Value, time = item.Time });
                }
            }
            catch (Exception) { }
        }
        public IList<HddMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics").ToList();
            }
        }
        public IList<HddMetric> GetByTimePeriod (long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
