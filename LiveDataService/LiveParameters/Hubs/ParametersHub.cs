using LiveDataService.LiveParameters.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LiveDataService.LiveParameters.Hubs
{
    public class ParametersHub: Hub
    {
        private ClientConnectionHandler _connectionHandler;
        public ParametersHub(ClientConnectionHandler connectionHandler) 
        {
            _connectionHandler = connectionHandler;
        }


        public override Task OnDisconnectedAsync(Exception exception)
        {
            string clientId = _connectionHandler.GetClientConnection(Context.ConnectionId).ClientId;
            _connectionHandler.DisconnectClient(Context.ConnectionId);
            Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith((task) => _connectionHandler.DeleteClientConfig(clientId));
            Debug.WriteLine("DISCONNTED CLIENT");
            return Task.CompletedTask;
        }


        public async Task StartTransfer(string clientId)
        {
            bool isConnectionSuccessful = _connectionHandler.ConnectClient(clientId, Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("connectionStatus", isConnectionSuccessful);
        }

        

    }
}
