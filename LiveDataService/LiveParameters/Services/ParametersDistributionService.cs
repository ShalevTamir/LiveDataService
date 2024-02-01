using LiveDataService.LiveParameters.Hubs;
using LiveDataService.LiveParameters.Models;
using LiveDataService.LiveParameters.Models.Dtos;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveDataService.LiveParameters.Services
{
    public class ParametersDistributionService
    {
        private ClientConnectionHandler _connectionHandler;
        private IHubContext<ParametersHub> _hubContext;
        public ParametersDistributionService(ClientConnectionHandler connectionHandler, IHubContext<ParametersHub> hubContext) 
        { 
            _connectionHandler = connectionHandler;
            _hubContext = hubContext;
        }

        public async Task ProccessTeleData(TelemetryFrameDto telemetryFrame)
        {
            //parallel foreach
            foreach(ClientConnection client in _connectionHandler.GetConnectedClients())
            {
                IEnumerable<TelemetryParameterDto> filteredParameters = 
                    telemetryFrame.Parameters.Where(parameter => client.ParameterNames.Contains(parameter.Name));

                await _hubContext.Clients.Client(client.ConnectionId).SendAsync(
                    "receiveParameters",
                    new FilteredFrameDto()
                    {
                        Parameters = filteredParameters,
                        TimeStamp = new DateTimeOffset(telemetryFrame.TimeStamp).ToUnixTimeMilliseconds()
                    }
                    );
            }
                

        }
    }
}
