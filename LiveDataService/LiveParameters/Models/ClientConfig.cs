using System;
using System.Collections.Generic;

namespace LiveDataService.LiveParameters.Models
{
    public class ClientConfig
    {
        public string ConnectionId {  get; private set; }
        public HashSet<string> ParameterNames { get; private set; }

        public ClientConfig(HashSet<string> parameterNames)
        {            
            ConnectionId = Guid.NewGuid().ToString();
            ParameterNames = parameterNames;
        }

        public ClientConfig(List<string> parameterNames)
        {
            ConnectionId = Guid.NewGuid().ToString();
            ParameterNames = new HashSet<string>(parameterNames);
        }
      
    }
}
