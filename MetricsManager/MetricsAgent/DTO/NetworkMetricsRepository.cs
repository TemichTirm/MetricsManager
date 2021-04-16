using Dapper;
using MetricsAgent.Models;
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
        // наше соединение с базой данных
        private readonly SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public NetworkMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new { value = item.Value, time = item.Time });
            }
        }
        public IList<NetworkMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();
            }
        }
        public IList<NetworkMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
