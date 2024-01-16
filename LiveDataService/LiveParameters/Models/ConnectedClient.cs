using System.Collections.Generic;

namespace LiveDataService.LiveParameters.Models
{
    public class ConnectedClient
    {
        public readonly string _connectionId;
        public readonly HashSet<string> _parameterNames;
    }
}
