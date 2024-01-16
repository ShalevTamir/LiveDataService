using LiveDataService.LiveParameters.Hubs;
using LiveDataService.LiveParameters.Models;
using LiveDataService.LiveParameters.Models.Dtos;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LiveDataService.LiveParameters.Services
{
    public class ParametersFilterService
    {
        private readonly IHubContext<ParametersHub> _hub;
        private readonly List<ConnectedClient> _connectedClients;
 
        public ParametersFilterService(IHubContext<ParametersHub> hub) 
        {
            _hub = hub;
        }

        public void ConnectClient(ConnectedClient client)
        {
            _connectedClients.Add(client);
        }

        public void DisconnectClient(ConnectedClient client)
        {
            _connectedClients.Remove(client);
        }

        public void ProcessTeleData(string JTeleData)
        {
            var telemetryFrame = JsonConvert.DeserializeObject<TelemetryFrameDto>(JTeleData);
            foreach(var client in _connectedClients)
            {
                IEnumerable<TelemetryParameterDto> filteredParameters = telemetryFrame.Parameters
                    .Where(parameter => client._parameterNames.Contains(parameter.Name));

                _hub.Clients
                    .Client(client._connectionId)
                    .SendAsync(JsonConvert.SerializeObject(filteredParameters));
            }

        }
    }
}
