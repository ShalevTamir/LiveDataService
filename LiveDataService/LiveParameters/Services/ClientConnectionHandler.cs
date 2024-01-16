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
    public class ClientConnectionHandler
    {
        private readonly List<ClientConfig> _clientConfigurations;
        private readonly Dictionary<ClientConfig, string> _connectedClients;

        public ClientConnectionHandler() 
        {
            _clientConfigurations = new List<ClientConfig>();
            _connectedClients = new Dictionary<ClientConfig, string>();
        }

        public void ConfigClient(ClientConfig client)
        {
            _clientConfigurations.Add(client);
        }

        public void DeleteClientConfig(ClientConfig client)
        {
            _clientConfigurations.Remove(client);
        }

        public bool ConnectClient(string clientId, string connectionId)
        {
            var client = _clientConfigurations.Find(client => client.ConnectionId == clientId);

            if(client != null) 
            {
                _connectedClients.Add(client, connectionId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisconnectClient(ClientConfig client)
        {
            _connectedClients.Remove(client);
        }
    }
}
