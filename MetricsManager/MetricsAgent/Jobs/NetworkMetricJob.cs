using MetricsAgent.DTO;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        // Инжектируем DI провайдер
        private readonly IServiceProvider _provider;
        private INetworkMetricsRepository _repository;
        private int count = 0;
        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<INetworkMetricsRepository>();
        }
        public Task Execute(IJobExecutionContext context)
        {
            count++;
            _repository.Create(new Models.NetworkMetric { Time = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(), Value = count*2 });
            return Task.CompletedTask;
        }
    }
}
