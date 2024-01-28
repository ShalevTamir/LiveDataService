using System;
using System.Collections.Generic;
using System.Threading;

namespace LiveDataService.LiveParameters.Models
{
    public class ClientConnection
    {
        public string ClientId {  get; private set; }
        public string ConnectionId { get; private set; }
        public HashSet<string> ParameterNames { get; private set; }
        public CancellationTokenSource cancelDisconnectToken { get; private set; }
        

        public ClientConnection(HashSet<string> parameterNames)
        {            
            ClientId = Guid.NewGuid().ToString();
            ParameterNames = parameterNames;
            //change to default
            ConnectionId = null;
            cancelDisconnectToken = new CancellationTokenSource();
        }

        public ClientConnection(List<string> parameterNames) :
            this(new HashSet<string>(parameterNames))
        {
        }

        public void Connect(string connectionId)
        {
            cancelDisconnectToken.Cancel();
            cancelDisconnectToken = new CancellationTokenSource();
            this.ConnectionId = connectionId;
        }

        public void Disconnect() 
        {
            this.ConnectionId = null;
        }

        public bool IsConnected() => this.ConnectionId != null;
      
    }
}
