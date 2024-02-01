using LiveDataService.LiveParameters.Models.Dtos;
using LiveDataService.LiveParameters.Services;
using LiveDataService.Mongo.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LiveDataService.Consumer.Services
{
    public class TeleProcessorService
    {
        private ParametersDistributionService _parametersDistributionService;
        private MongoFramesService _mongoFramesService;

        public TeleProcessorService(ParametersDistributionService parametersDistributionService, MongoFramesService mongoFramesService)
        {
            _parametersDistributionService = parametersDistributionService;
            _mongoFramesService = mongoFramesService;
        }

        public async Task ProcessTeleData(string JTeleData)
        {
            var telemetryFrame = JsonConvert.DeserializeObject<TelemetryFrameDto>(JTeleData);
            if (telemetryFrame == null)
                throw new ArgumentException("Unable to deserialize telemetry frame \n" + JTeleData + "");

            await _parametersDistributionService.ProccessTeleData(telemetryFrame);
            await _mongoFramesService.InsertAsync(new Mongo.Models.Frame()
            {
                Parameters = telemetryFrame.Parameters.ToList(),
                TimeStamp = telemetryFrame.TimeStamp.ToString(),
            });
        }
    }
}
