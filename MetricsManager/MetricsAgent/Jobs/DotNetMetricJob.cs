using MetricsAgent.DTO;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private IDotNetMetricsRepository _repository;
        private int count = 0;
        public DotNetMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<IDotNetMetricsRepository>();
        }
        public Task Execute(IJobExecutionContext context)
        {
            count++;
            _repository.Create(new Models.DotNetMetric { Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(), Value = count*2 });
            return Task.CompletedTask;
        }
    }
}
