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
        private ParametersDistributionService _parametersDistributionService;
        public StartupService(KafkaConsumerService kafkaConsumerService, ParametersDistributionService parametersDistributionService)
        {
            _kafkaConsumerService = kafkaConsumerService;
            _parametersDistributionService = parametersDistributionService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _kafkaConsumerService.StartConsumer((JTeleData) => _parametersDistributionService.ProccessTeleData(JTeleData).Wait());
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
