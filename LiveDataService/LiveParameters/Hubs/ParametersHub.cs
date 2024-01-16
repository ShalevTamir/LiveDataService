using LiveDataService.LiveParameters.Services;
using Microsoft.AspNetCore.SignalR;
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

        public async Task StartTransfer(string clientId)
        {
            bool isConnectionSuccessful = _connectionHandler.ConnectClient(clientId, Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("connectionStatus", isConnectionSuccessful);
        }

        

    }
}
