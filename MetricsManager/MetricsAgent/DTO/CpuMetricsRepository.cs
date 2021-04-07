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
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        // наше соединение с базой данных
        private readonly SQLiteConnection _connection;

        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public CpuMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
            //SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                // запрос на вставку данных с плейсхолдерами для параметров
                _connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
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

        public void Delete(int metricId)
        {
            using (var connection = new SQLiteConnection(_connection))
            // прописываем в команду SQL запрос на удаление данных
            {
                connection.Execute("DELETE FROM cpumetrics WHERE id=@id", new { id = metricId });
            }
        }

        public void Update(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                // прописываем в команду SQL запрос на обновление данных
                connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id;",
                     new { value = item.Value, time = item.Time, id = item.Id });
            }
        }

        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                // читаем при помощи Query и в шаблон подставляем тип данных
                // объект которого Dapper сам и заполнит его поля
                // в соответсвии с названиями колонок
                return connection.Query<CpuMetric>("SELECT id, time, value FROM cpumetrics").ToList();
            }
        }        

        public CpuMetric GetById(int metricId)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                try
                {
                    return connection.QuerySingle<CpuMetric>("SELECT * FROM cpumetrics WHERE id = @id",
                    new { id = metricId });
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public IList<CpuMetric> GetByTimePeriod (long getFromTime, long getToTime)
        {
            using (var connection = new SQLiteConnection(_connection))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE (time>=@fromTime) AND (time<=@toTime)",
                    new { fromTime = getFromTime, toTime = getToTime }).ToList();
            }
        }
    }
}
