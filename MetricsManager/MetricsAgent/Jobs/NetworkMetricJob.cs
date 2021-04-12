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
        private readonly INetworkMetricsRepository _repository;
        private readonly PerformanceCounter[] _networkCounters;

        public NetworkMetricJob(IServiceProvider provider)
        {
            _provider = provider;
            _repository = _provider.GetService<INetworkMetricsRepository>();
            var Category = new PerformanceCounterCategory("Network Interface");
            string[] instanceNames = Category.GetInstanceNames();
            _networkCounters = new PerformanceCounter[instanceNames.Length];
            int count = 0;
            foreach (var instance in instanceNames)
            {
                _networkCounters[count] = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
                count++;
            }
        }
        public Task Execute(IJobExecutionContext context)
        {

            int totalBytesRecieved = 0;
            foreach (var item in _networkCounters)
            {
                totalBytesRecieved += Convert.ToInt32(item.NextValue());
            }
            var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _repository.Create(new Models.NetworkMetric { Time = time, Value = totalBytesRecieved });
            return Task.CompletedTask;
        }
    }
}
