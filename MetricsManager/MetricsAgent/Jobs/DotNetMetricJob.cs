using MetricsAgent.DTO;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    //[DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private readonly IDotNetMetricsRepository _repository;
        private readonly PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all heaps", "_Global_"); 
        }
        public Task Execute(IJobExecutionContext context)
        {
            var allHeapSizeInKBytes = Convert.ToInt32(_dotNetCounter.NextValue()/1024);
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.DotNetMetric { Time = time, Value = allHeapSizeInKBytes });
            return Task.CompletedTask;
        }
    }
}
