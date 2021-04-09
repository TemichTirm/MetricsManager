using MetricsAgent.DTO;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private ICpuMetricsRepository _repository;
        private int count = 0;
        public CpuMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<ICpuMetricsRepository>();
        }
        public Task Execute(IJobExecutionContext context)
        {
            count++;
            _repository.Create(new Models.CpuMetric { Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(), Value = count*2 });
            return Task.CompletedTask;
        }
    }
}
