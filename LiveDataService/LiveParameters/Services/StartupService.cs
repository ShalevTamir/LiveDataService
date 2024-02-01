using LiveDataService.Consumer.Services;
using LiveDataService.Mongo.Services;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LiveDataService.LiveParameters.Services
{
    public class StartupService : IHostedService
    {
        private KafkaConsumerService _kafkaConsumerService;
        private TeleProcessorService _teleProcessorService;
        public StartupService(KafkaConsumerService kafkaConsumerService, TeleProcessorService teleProcessorService)
        {
            _kafkaConsumerService = kafkaConsumerService;
            _teleProcessorService = teleProcessorService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _kafkaConsumerService.StartConsumer((JTeleData) => _teleProcessorService.ProcessTeleData(JTeleData).Wait());
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
