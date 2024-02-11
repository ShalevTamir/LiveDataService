using LiveDataService.LiveParameters.Models;
using LiveDataService.LiveParameters.Models.Dtos;
using LiveDataService.LiveParameters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

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

        [HttpPost("config")]
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

        [HttpPost("add-parameter")]
        public ActionResult AddParameter([FromBody] AddParameterDto addParameterDto)
        {
            ClientConnection? clientConnection = _clientConnectionHandler.GetConnectionByClientId(addParameterDto.ClientId);
            if (clientConnection == null)
            {
                return BadRequest("Client isn't registered in the server");
            }
            else
            {
                clientConnection.AddParameter(addParameterDto.ParameterName);
                return Ok();
            }

        }
    }
}
