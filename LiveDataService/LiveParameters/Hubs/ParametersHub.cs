using LiveDataService.LiveParameters.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace LiveDataService.LiveParameters.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParametersHub: Hub
    {
        public static readonly string Endpoint = "/live-parameters-socket";
        private ClientConnectionHandler _connectionHandler;
        public ParametersHub(ClientConnectionHandler connectionHandler) 
        {
            _connectionHandler = connectionHandler;
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var clientConnection = _connectionHandler.GetConnectionByConnectionId(Context.ConnectionId);
            if(clientConnection != null)
            {
                var cancellationToken = clientConnection.cancelDisconnectToken.Token;
                string clientId = clientConnection.ClientId;
                _connectionHandler.DisconnectClient(Context.ConnectionId);
                await Task.Delay(
                    TimeSpan.FromMinutes(5)
                    ).ContinueWith((task) => {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            Debug.WriteLine("deleted");
                            _connectionHandler.DeleteClientConfig(clientId); 
                        }
                    });
            }
        }


        public async Task StartTransfer(string clientId)
        {
            bool isConnectionSuccessful = _connectionHandler.ConnectClient(clientId, Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).SendAsync("connectionStatus", isConnectionSuccessful);
        }

        

    }
}
