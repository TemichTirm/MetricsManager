﻿using Dapper;
using MetricsAgent.Models;
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
        // наше соединение с базой данных
        private readonly SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public DotNetMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                new { value = item.Value, time = item.Time });
            }
        }

        public void Delete(int metricId)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id", new { id = metricId });
            }
        }

        public void Update(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id=@id;",
                new { value = item.Value, time = item.Time, id = item.Id });
            }
        }

        public IList<DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
            }            
        }

        public DotNetMetric GetById(int metricId)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.QuerySingle<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id = @id",
                new { id = metricId });
            }            
        }

        public IList<DotNetMetric> GetByTimePeriod(long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
