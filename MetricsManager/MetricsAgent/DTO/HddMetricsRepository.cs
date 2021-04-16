using Dapper;
using MetricsAgent.Models;
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
        // наше соединение с базой данных
        private readonly SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public HddMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }
        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                new { value = item.Value, time = item.Time });
            }
        }
        public IList<HddMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics").ToList();
            }
        }
        public IList<HddMetric> GetByTimePeriod (long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
