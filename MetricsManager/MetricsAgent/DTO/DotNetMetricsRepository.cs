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
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {

    }
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        /// <summary>
        /// Добавление в базу данных новой метрики .Net 
        /// (счетчик текущего объема памяти, выделенной в килобайтах для куч сборки мусора) 
        /// </summary>
        /// <param name="item">Объект класса DotNetMetric</param>
        public void Create(DotNetMetric item)
        {
            try
            {
                using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
                {
                    connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    new { value = item.Value, time = item.Time });
                }
            }
            catch (Exception) { }
        }
        public IList<DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
            }            
        }
        public IList<DotNetMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
