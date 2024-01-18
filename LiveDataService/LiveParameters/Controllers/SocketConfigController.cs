using LiveDataService.LiveParameters.Hubs;
using LiveDataService.LiveParameters.Models;
using LiveDataService.LiveParameters.Models.Dtos;
using LiveDataService.LiveParameters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace LiveDataService.LiveParameters.Controllers
{
    [Route("socket-config")]
    [ApiController]
    public class SocketConfigController : Controller
    {
        private readonly ClientConnectionHandler _clientConnectionHandler;

        public SocketConfigController(ClientConnectionHandler clientConnectionHandler)
        {
            _clientConnectionHandler = clientConnectionHandler;
        }

        [HttpPost]
        public ActionResult ConfigSocket([FromBody] ParametersListDto parametersList)
        {
            ClientConnection client = new ClientConnection(parametersList.ParameterNames); 
            _clientConnectionHandler.ConfigClient(client);

            return Ok(
                JsonConvert.SerializeObject(
                    new ClientConnectionId() { ConnectionId = client.ClientId }
                    )
                );
        }
    }
}
