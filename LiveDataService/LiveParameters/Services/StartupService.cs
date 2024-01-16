using LiveDataService.Consumer.Services;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LiveDataService.LiveParameters.Services
{
    public class StartupService : IHostedService
    {
        private KafkaConsumerService _kafkaConsumerService;
        private ParametersFilterService _parametersFilterService;
        public StartupService(KafkaConsumerService kafkaConsumerService, ParametersFilterService parametersFilterService)
        {
            _kafkaConsumerService = kafkaConsumerService;
            _parametersFilterService = parametersFilterService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _kafkaConsumerService.StartConsumer(_parametersFilterService.ProcessTeleData);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void PrintConsumerData(string data)
        {
            Debug.WriteLine(data);
        }
    }
}
