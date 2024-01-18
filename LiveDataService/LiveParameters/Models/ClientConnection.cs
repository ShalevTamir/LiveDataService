using System;
using System.Collections.Generic;

namespace LiveDataService.LiveParameters.Models
{
    public class ClientConnection
    {
        public string ClientId {  get; private set; }
        public string ConnectionId { get; private set; }
        public HashSet<string> ParameterNames { get; private set; }
        

        public ClientConnection(HashSet<string> parameterNames)
        {            
            ClientId = Guid.NewGuid().ToString();
            ParameterNames = parameterNames;
            //change to default
            ConnectionId = null;
        }

        public ClientConnection(List<string> parameterNames) :
            this(new HashSet<string>(parameterNames))
        {
        }

        public void Connect(string connectionId)
        {
            this.ConnectionId = connectionId;
        }

        public void Disconnect() 
        {
            this.ConnectionId = null;
        }

        public bool IsConnected() => this.ConnectionId != null;
      
    }
}
