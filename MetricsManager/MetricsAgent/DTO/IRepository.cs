using System;
using System.Collections.Generic;

namespace MetricsAgent.DTO
{
    public interface Repository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IList<T> GetByTimePeriod(double fromTime, double toTime);
    }
}
