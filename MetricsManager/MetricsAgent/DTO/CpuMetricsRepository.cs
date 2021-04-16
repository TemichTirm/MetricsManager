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
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        
        public void Create(CpuMetric item)
        {
            try
            {
                using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
                {
                    // запрос на вставку данных с плейсхолдерами для параметров
                    connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,
                        // записываем в поле time количество секунд
                        time = item.Time
                    });
                }
            }
            catch (Exception) { }          
        }

        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                // читаем при помощи Query и в шаблон подставляем тип данных
                // объект которого Dapper сам и заполнит его поля
                // в соответсвии с названиями колонок
                return connection.Query<CpuMetric>("SELECT id, time, value FROM cpumetrics").ToList();
            }
        }   
        public IList<CpuMetric> GetByTimePeriod (long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
