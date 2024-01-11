using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveDataService.LiveParameters.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LiveParametersController : Controller
    {
        [HttpGet()]
        public async Task GetParameters()
        {
            if(HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                string message = "This is a message from the socket";
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                var arraySegment = new ArraySegment<byte>(messageBytes, 0, messageBytes.Length);
                await webSocket.SendAsync(
                    arraySegment,
                    System.Net.WebSockets.WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                    );


            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
