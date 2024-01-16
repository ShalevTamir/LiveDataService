using LiveDataService.LiveParameters.Hubs;
using LiveDataService.LiveParameters.Models;
using LiveDataService.LiveParameters.Models.Dtos;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;

namespace LiveDataService.LiveParameters.Services
{
    public class ClientConnectionHandler
    {
        private readonly List<ClientConnection> _clientConnections;

        public ClientConnectionHandler() 
        {
            _clientConnections = new List<ClientConnection>();
        }

        public void ConfigClient(ClientConnection client)
        {
            _clientConnections.Add(client);
        }

        public void DeleteClientConfig(string clientId)
        {
            var client = _clientConnections.Find(client => client.ClientId == clientId);
            if (client != null) 
            { 
                _clientConnections.Remove(client);
            }
        }

        public bool ConnectClient(string clientId, string connectionId)
        {
            var client = _clientConnections.Find(client => client.ClientId == clientId);
            client?.Connect(connectionId);
            return client != null;
        }

        public void DisconnectClient(string connectionId)
        {
            var client = _clientConnections.Find(client => client.ConnectionId == connectionId);
            client?.Disconnect();
        }

        public IEnumerable<ClientConnection> GetConnectedClients()
        {
            return _clientConnections.Where(client => client.IsConnected());
        }

        public ClientConnection GetClientConnection(string connectionId)
        {
            return _clientConnections.Find(client => client.ConnectionId == connectionId);
        }

    }
}
