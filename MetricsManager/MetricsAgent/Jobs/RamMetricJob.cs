using MetricsAgent.DTO;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private IRamMetricsRepository _repository;
        private int count = 0;
        public RamMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IRamMetricsRepository>();
        }
        public Task Execute(IJobExecutionContext context)
        {
            count++;
            _repository.Create(new Models.RamMetric { Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(), Value = count*2 });
            return Task.CompletedTask;
        }
    }
}
