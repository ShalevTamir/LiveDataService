using LiveDataService.Mongo.Models.FramesController;
using LiveDataService.Mongo.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LiveDataService.Mongo.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class FramesController: Controller
    {
        private MongoFramesService _mongoFramesService;
        public FramesController(MongoFramesService mongoFramesService)
        {
            _mongoFramesService = mongoFramesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetFrames([FromQuery]GetFramesQueryParams queryParams)
        {            
            return Ok(JsonConvert.SerializeObject(await _mongoFramesService.GetFrames(
                queryParams.MinTimeStamp,
                queryParams.MaxTimeStamp,
                queryParams.MaxSamplesInPage,
                queryParams.PageNumber
                )));   
        }
    }
}
