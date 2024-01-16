using LiveDataService.LiveParameters.Hubs;
using LiveDataService.LiveParameters.Models.Dtos;
using LiveDataService.LiveParameters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace LiveDataService.LiveParameters.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LiveParametersController : Controller
    {
        private readonly ParametersFilterService _parametersFilterService;

        public LiveParametersController(IHubContext<ParametersHub> hub, ParametersFilterService parametersFilterService)
        {
            _parametersFilterService = parametersFilterService;
        }

        [HttpGet]
        public ActionResult InitiateSocket(ParametersListDto parametersList)
        {
            
            return Ok(JsonConvert.SerializeObject(parametersList));
        }
    }
}
