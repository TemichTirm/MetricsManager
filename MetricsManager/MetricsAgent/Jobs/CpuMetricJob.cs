using MetricsAgent.DTO;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{ 
    //[DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly ICpuMetricsRepository _repository;
        private readonly PerformanceCounter _cpuCounter;
        public CpuMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<ICpuMetricsRepository>();
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            // узнаем когда мы сняли значение метрики.
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.CpuMetric { Time = time, Value = cpuUsageInPercents });
            return Task.CompletedTask;
        }
    }
}
